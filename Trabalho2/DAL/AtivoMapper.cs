using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AtivoMapper
    {
        private String cs;
        public AtivoMapper()
        {
            cs = ConfigurationManager.ConnectionStrings["base dados"].ConnectionString;
            if (cs == null)
            {
                throw new Exception("Connection String must be configured in the config file!");
            }
        }

        public List<Ativo> GetAtivos()
        {
            var ativos = new List<Ativo>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cs))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("select * from Ativo", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.Text;

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Ativo ativo = new Ativo
                                {
                                    id = sqlDataReader.SafeGet<int>(0),
                                    parent_id = sqlDataReader.SafeGet<int>(1),
                                    nome = sqlDataReader.SafeGet<string>(2),
                                    data_aquisicao = sqlDataReader.SafeGet<DateTime>(3),
                                    marca = sqlDataReader.SafeGet<string>(4),
                                    modelo = sqlDataReader.SafeGet<string>(5),
                                    localizacao = sqlDataReader.SafeGet<string>(6),
                                    estado = sqlDataReader.SafeGet<int>(7),
                                    id_tipo = sqlDataReader.SafeGet<int>(8)
                                };

                                ativos.Add(ativo);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw exception;
            }
            return ativos;
        }
    }
}

