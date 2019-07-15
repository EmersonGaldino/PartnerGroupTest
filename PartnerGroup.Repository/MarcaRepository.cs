using PartnerGroup.Domain.Models;
using PartnerGroup.Domain.Repository;
using PartnerGroup.Infra;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PartnerGroup.Repository
{
    public class MarcaRepository : IRepository<Marca>
    {
        public Marca Create(Marca newEntity)
        {
            if (!CanCreateMarca(newEntity))
                return new Marca();

            using (var factory = new ConnectionFactory())
            {
                var cmdText = "STP_REGISTER_MARCA";
                var parametros = new Dictionary<string, object>
                {
                    {"@nome"    , newEntity.Nome}
                };
                var result = factory.ExecuteScalar(cmdText, CommandType.StoredProcedure, parametros);
                int.TryParse(result.ToString(), out int newMarcaId);

                if (newMarcaId == 0)
                    throw new Exception("Erro ao gravar usuário no banco");
                return GetSingle(newMarcaId);
            }
        }

        private bool CanCreateMarca(Marca newMarca)
        {
            return newMarca != null && (
            !string.IsNullOrEmpty(newMarca.Nome)
            );
        }



        public IEnumerable<Marca> GetAll()
        {
            try
            {
                using (var factory = new ConnectionFactory())
                {
                    var sqlText = "STP_GETALL_MARCA";
                    return GetAllFromReader(factory.GetReader(sqlText, CommandType.StoredProcedure));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public IEnumerable<Marca> GetAllFromReader(IDataReader reader)
        {
            var result = new List<Marca>();
            while (reader.Read())
            {
                var entityUser = GetEntityFromReader(reader);
                if (entityUser.Id > 0)
                    result.Add(entityUser);
            }
            return result;
        }


        public Marca GetEntityFromReader(IDataReader reader)
        {
            try
            {
                int.TryParse(reader["ID"].ToString(), out int MarcaId);

                return new Marca()
                {
                    Id = MarcaId,               
                    Nome = reader["NOME"].ToString()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Marca GetSingle(int MarcaId)
        {
            var usuarioFromReader = new Marca();
            var sqlText = "STP_GET_Marca";
            var parametros = new Dictionary<string, object>() {
                { "@Marca_id",MarcaId }
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

            if (GetSingle(entityId).Id < 1)
            {
                return false;
            }

            using (var factory = new ConnectionFactory())
            {
                var cmdText = "delete dbo.marca where [id]=@marca_id";
                var parametros = new Dictionary<string, object>
                {
                    {"@marca_id"    , entityId}
                };
                var result = factory.ExecuteNonQuery(cmdText, CommandType.Text, parametros);
                return true;
            }
        }

        public Marca Update(Marca updatedEntity, int id)
        {
            if (string.IsNullOrEmpty(updatedEntity.Nome))
                return null;

            using (var factory = new ConnectionFactory())
            {
                var cmdText = "update dbo.marca set [nome]=@nome";
                var parametros = new Dictionary<string, object>
                {
                    {"@nome"        , updatedEntity.Nome}
                };
                var result = factory.ExecuteNonQuery(cmdText, CommandType.Text, parametros);
                return GetSingle(updatedEntity.Id);
            }
        }
    }
}
