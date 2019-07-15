using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PartnerGroup.Infra
{
    public class ConnectionFactory : IDisposable
    {
        private SqlConnection Connection;
        private const string CONN_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PartnerGroupDB;Integrated Security=True";

        private SqlConnection GetConnection()
        {
            try
            {


                if (Connection == null)
                    Connection = new SqlConnection(CONN_STRING);

                if (Connection.State != ConnectionState.Open)
                    Connection.Open();

                return Connection;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public SqlCommand GetCommand()
        {
            return this.GetConnection().CreateCommand();
        }

        public void Dispose()
        {
            if (Connection != null && Connection.State == ConnectionState.Open)
                Connection.Close();
            Connection.Dispose();
        }

        public SqlDataReader GetReader(string cmdText,
            CommandType cmdType = CommandType.Text,
            Dictionary<string, object> parametros = null
            )
        {
            using (var cmd = this.GetCommand())
            {
                cmd.CommandText = cmdText;
                cmd.CommandType = cmdType;

                if (parametros != null)
                    foreach (var pr in parametros)
                        cmd.Parameters.AddWithValue(pr.Key, pr.Value);

                return cmd.ExecuteReader();
            }
        }

        public bool ExecuteNonQuery(string cmdText,
            CommandType cmdType = CommandType.Text,
            Dictionary<string, object> parametros = null)
        {
            try
            {
                using (var cmd = this.GetCommand())
                {
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdType;

                    if (parametros != null)
                        foreach (var pr in parametros)
                            cmd.Parameters.AddWithValue(pr.Key, pr.Value);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public object ExecuteScalar(string cmdText,
            CommandType cmdType = CommandType.Text,
            Dictionary<string, object> parametros = null)
        {
            try
            {
                using (var cmd = this.GetCommand())
                {
                    cmd.CommandText = cmdText;
                    cmd.CommandType = cmdType;

                    if (parametros != null)
                        foreach (var pr in parametros)
                            cmd.Parameters.AddWithValue(pr.Key, pr.Value);
                    cmd.Parameters.Add("@retorno", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.ExecuteScalar();
                    int patrimonioId = Convert.ToInt32(cmd.Parameters["@retorno"].Value);

                    return patrimonioId;

                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
