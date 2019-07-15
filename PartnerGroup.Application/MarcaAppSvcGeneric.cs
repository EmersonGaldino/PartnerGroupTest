using Newtonsoft.Json;
using PartnerGroup.Domain.Models;
using PartnerGroup.Domain.Service;
using PartnerGroup.Repository;
using PartnerGroup.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PartnerGroup.Services
{
    public class MarcaAppSvcGeneric : IGenericService<Marca>
    {
        private MarcaRepository rep = new MarcaRepository();

        private APIReturn result;

        public APIReturn Create(Marca toCreate)
        {

            try
            {
                result = new APIReturn
                {
                    Content = JsonConvert.SerializeObject(rep.Create(toCreate)),
                    Message = string.IsNullOrEmpty(ObjectValidator.MarcaValidator(toCreate)) ? "Adicionado com sucesso" : "Dados incorretos",
                    ErrorMessage = ObjectValidator.MarcaValidator(toCreate)
                };
            }
            catch (Exception ex)
            {
                result = new APIReturn
                {
                    StatusCode = 500,
                    Message = "Ocorreu um erro inesperado ao tendar criar o registro",
                    ErrorMessage = ex.Message
                };
            }

            return result;
        }

        public APIReturn Delete(int id)
        {
            try
            {

                bool isDeleted = rep.Delete(id);

                result = new APIReturn
                {
                    Content = isDeleted == true ? true : false,
                    Message = isDeleted == true ? "Excluído com sucesso" : "Não foi possível excluir",
                    ErrorMessage = isDeleted == false ? "Marca informada inexistente" : string.Empty
                };
            }
            catch (Exception ex)
            {
                result = new APIReturn
                {
                    StatusCode = 500,
                    Message = "Ocorreu um erro inesperado ao tendar deletar",
                    ErrorMessage = ex.Message
                };
            }

            return result;
        }


        public APIReturn GetSingle(int id)
        {
            try
            {
                result = new APIReturn
                {
                    Content = JsonConvert.SerializeObject(rep.GetSingle(id)),
                    Message = rep.GetSingle(id).Id > 0 ? "Registro recuperado com sucesso" : "Erro ao recuperar registro",
                    ErrorMessage = "Não há resultado para a busca solicitada"
                };
            }
            catch (Exception ex)
            {
                result = new APIReturn
                {
                    StatusCode = 500,
                    Message = "Ocorreu um erro inesperado ao tendar recuperar o registro",
                    ErrorMessage = ex.Message
                };
            }

            return result;
        }

        public APIReturn GetAll()
        {

            try
            {
                result = new APIReturn
                {
                    Content = JsonConvert.SerializeObject(rep.GetAll()),
                    Message = rep.GetAll().Count() > 0 ? "Lista recuperada com sucesso" : "Não existem dados para serem exibidos"
                };
            }
            catch (Exception ex)
            {
                result = new APIReturn
                {
                    StatusCode = 500,
                    Message = "Ocorreu um erro inesperado ao tendar recuperar os registros",
                    ErrorMessage = ex.Message
                };
            }

            return result;

        }


        public APIReturn Update(Marca toUpdate, int id)
        {


            try
            {

                var currentEntity = rep.GetSingle(id);
                currentEntity.Nome = toUpdate.Nome;

                result = new APIReturn
                {
                    Content = JsonConvert.SerializeObject(rep.Update(currentEntity, id)),
                    Message = string.IsNullOrEmpty(ObjectValidator.MarcaValidator(toUpdate)) ? "Atualizado com sucesso" : "Dados incorretos",
                    ErrorMessage = ObjectValidator.MarcaValidator(toUpdate)
                };
            }
            catch (Exception ex)
            {
                result = new APIReturn
                {
                    StatusCode = 500,
                    Message = "Ocorreu um erro inesperado ao tendar atualizar o registro",
                    ErrorMessage = ex.Message
                };
            }

            return result;

        }

    }
}

