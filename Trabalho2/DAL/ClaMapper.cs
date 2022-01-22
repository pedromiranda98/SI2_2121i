/*using Entities;
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
    public class ClaMapper
    {
        private String cs;
        public ClaMapper()
        {
            cs = ConfigurationManager.ConnectionStrings["base dados"].ConnectionString;
            if (cs == null)
            {
                throw new Exception("Connection String must be configured in the config file!");
            }
        }

        public List<Cla> GetClas()
        {
            var clas = new List<Cla>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cs))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("select * from CLA", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.Text;

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                Cla c = new Cla
                                {
                                    nome = sqlDataReader.SafeGet<string>(0),
                                    pontuacao = sqlDataReader.SafeGet<decimal>(1),
                                    max_jog = sqlDataReader.SafeGet<int>(2)
                                };

                                clas.Add(c);
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
            return clas;
        }
    }
}*/
 
