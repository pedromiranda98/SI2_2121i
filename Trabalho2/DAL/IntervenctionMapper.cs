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
    public class IntervenctionMapper
    {
        private String cs;
        public IntervenctionMapper()
        {
            cs = ConfigurationManager.ConnectionStrings["base dados"].ConnectionString;
            if (cs == null)
            {
                throw new Exception("Connection String must be configured in the config file!");
            }
        }

          public List<Intervencao> getIntervenction()
          {
            var intervencoes = new List<Intervencao>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cs))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("select * from Intervencao", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.Text;

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Intervencao interv = new Intervencao
                                {
                                    id = sqlDataReader.SafeGet<int>(0),
                                    descricao = sqlDataReader.SafeGet<string>(1),
                                    estado = sqlDataReader.SafeGet<string>(2),
                                    valor = sqlDataReader.SafeGet<decimal>(3),
                                    data_inicio = sqlDataReader.SafeGet<DateTime>(4),
                                    data_fim = sqlDataReader.SafeGet<DateTime>(5),
                                    periodicidade = sqlDataReader.SafeGet<int>(6),
                                    ativo_id = sqlDataReader.SafeGet<int>(7)
                                
                                };

                                intervencoes.Add(interv);
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
            return intervencoes;
        }
    }
}

