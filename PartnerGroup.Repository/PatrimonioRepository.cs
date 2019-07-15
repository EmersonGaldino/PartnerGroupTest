using PartnerGroup.Domain.Models;
using PartnerGroup.Domain.Repository;
using PartnerGroup.Infra;
using System;
using System.Collections.Generic;
using System.Data;

namespace PartnerGroup.Repository
{
    public class PatrimonioRepository : IRepository<Patrimonio>
    {
        public Patrimonio Create(Patrimonio newEntity)
        {
            if (!CanCreatePatrimonio(newEntity))
                return new Patrimonio();

            using (var factory = new ConnectionFactory())
            {
                var cmdText = "STP_REGISTER_PATRIMONIO";
                var parametros = new Dictionary<string, object>
                {
                     {"@nome"     , newEntity.Nome}
                    ,{"@marca_id"    , newEntity.MarcaId}
                    ,{"@descricao"    , newEntity.Descricao}
                };
                var result = factory.ExecuteScalar(cmdText, CommandType.StoredProcedure, parametros);
                int.TryParse(result.ToString(), out int newPatrimonioId);

                if (newPatrimonioId == 0)
                    throw new Exception("Erro ao gravar usuário no banco");
                return GetSingle(newPatrimonioId);
            }
        }

        private bool CanCreatePatrimonio(Patrimonio newPatrimonio)
        {
            MarcaRepository rep = new MarcaRepository();

            return newPatrimonio != null && (
            !string.IsNullOrEmpty(newPatrimonio.Nome) &&
            rep.GetSingle(newPatrimonio.MarcaId).Id > 0
            );
        }


        public IEnumerable<Patrimonio> GetAll()
        {
            try
            {
                using (var factory = new ConnectionFactory())
                {
                    var sqlText = "STP_GETALL_PATRIMONIO";
                    return GetAllFromReader(factory.GetReader(sqlText, CommandType.StoredProcedure));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public IEnumerable<Patrimonio> GetAllFromReader(IDataReader reader)
        {
            var result = new List<Patrimonio>();
            while (reader.Read())
            {
                var entityUser = GetEntityFromReader(reader);
                if (entityUser.Id > 0)
                    result.Add(entityUser);
            }
            return result;
        }


        public Patrimonio GetEntityFromReader(IDataReader reader)
        {
            try
            {
                int.TryParse(reader["ID"].ToString(), out int patrimonioId);
                int.TryParse(reader["ID_MARCA"].ToString(), out int marcaId);
                int.TryParse(reader["NR_TOMBO"].ToString(), out int nrTombo);
                return new Patrimonio(nrTombo)
                {
                    Id = patrimonioId,
                    MarcaId = marcaId,
                    Descricao = reader["DESCRICAO"].ToString(),
                    Nome = reader["NOME"].ToString()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Patrimonio GetSingle(int PatrimonioId)
        {
            var usuarioFromReader = new Patrimonio();
            var sqlText = "STP_GET_PATRIMONIO";
            var parametros = new Dictionary<string, object>() {
                { "@patrimonio_id",PatrimonioId }
            };
            using (var factory = new ConnectionFactory())
            {
                var readerDaFactory = factory.GetReader(sqlText, CommandType.StoredProcedure, parametros);
                if (readerDaFactory.Read())
                    usuarioFromReader = GetEntityFromReader(readerDaFactory);
            }
            return usuarioFromReader;
        }

        public bool Delete(int entityId)
        {
            if (entityId == 0)
                return false;

            using (var factory = new ConnectionFactory())
            {
                var cmdText = "delete dbo.Patrimonio where [id]=@patrimonio_id";
                var parametros = new Dictionary<string, object>
                {
                    {"@patrimonio_id"    , entityId}
                };
                var result = factory.ExecuteNonQuery(cmdText, CommandType.Text, parametros);
                return true;
            }
        }

        public Patrimonio Update(Patrimonio updatedEntity, int id)
        {
            if (string.IsNullOrEmpty(updatedEntity.Nome) || updatedEntity.MarcaId == 0)
                return null;

            using (var factory = new ConnectionFactory())
            {
                var cmdText = "update dbo.Patrimonio set [nome]=@nome, [descricao]=@descricao, [id_marca]=@id_marca where [id]=@id";
                var parametros = new Dictionary<string, object>
                {
                    {"@nome"        , updatedEntity.Nome},
                    {"@id"          , id},
                    {"@id_marca"    , updatedEntity.MarcaId},
                    {"@descricao"   , updatedEntity.Descricao}
                };
                var result = factory.ExecuteNonQuery(cmdText, CommandType.Text, parametros);
                return GetSingle(updatedEntity.Id);
            }
        }

    }
}
