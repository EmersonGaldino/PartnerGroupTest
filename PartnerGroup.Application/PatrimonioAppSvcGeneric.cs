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
    public class PatrimonioAppSvcGeneric : IGenericService<Patrimonio>
    {
        private PatrimonioRepository rep = new PatrimonioRepository();
        private APIReturn result;

        public APIReturn Create(Patrimonio toCreate)
        {

            try
            {
                result = new APIReturn
                {
                    Content = rep.Create(toCreate),
                    Message = string.IsNullOrEmpty(ObjectValidator.PatrimonioValidator(toCreate)) ? "Adicionado com sucesso" : "Dados incorretos",
                    ErrorMessage = ObjectValidator.PatrimonioValidator(toCreate)
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
                    ErrorMessage = isDeleted == false ? "Patrimônio informado inexistente" : string.Empty
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
                    Content = rep.GetSingle(id),
                    Message = rep.GetSingle(id).Id > 0 ? "Registro recuperado com sucesso" : "Erro ao recuperar registro",
                    ErrorMessage = rep.GetSingle(id).Id > 0 ? string.Empty :  "Não há resultado para a busca solicitada"
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
                    Content = rep.GetAll(),
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


        public APIReturn Update(Patrimonio toUpdate, int id)
        {


            try
            {

                var currentEntity = rep.GetSingle(id);
                currentEntity.Nome = toUpdate.Nome;
                currentEntity.Descricao = toUpdate.Descricao;
                currentEntity.MarcaId = toUpdate.MarcaId;

                result = new APIReturn
                {
                    Content = rep.Update(currentEntity, id),
                    Message = string.IsNullOrEmpty(ObjectValidator.PatrimonioValidator(toUpdate)) ? "Atualizado com sucesso" : "Dados incorretos",
                    ErrorMessage = ObjectValidator.PatrimonioValidator(toUpdate)
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
