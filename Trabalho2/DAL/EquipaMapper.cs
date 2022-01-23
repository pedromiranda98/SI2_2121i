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
    public class EquipaMapper
    {
        private String cs;
        public EquipaMapper()
        {
            cs = ConfigurationManager.ConnectionStrings["base dados"].ConnectionString;
            if (cs == null)
            {
                throw new Exception("Connection String must be configured in the config file!");
            }
        }

        public List<Equipa> GetEquipa()
        {
            var equipas = new List<Equipa>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cs))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("select * from Equipa", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.Text;

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Equipa equipa = new Equipa
                                {
                                    id = sqlDataReader.SafeGet<int>(0),
                                    localizacao = sqlDataReader.SafeGet<string>(1),
                                    n_elemento = sqlDataReader.SafeGet<int>(2),
                                    intervencoes_atribuidas = sqlDataReader.SafeGet<int>(3)
                                };

                                equipas.Add(equipa);
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
            return equipas;
        }
    }
}
 
