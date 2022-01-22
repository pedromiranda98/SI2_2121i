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
    public class PartidaMapper
    {
        private String cs;
        public PartidaMapper()
        {
            cs = ConfigurationManager.ConnectionStrings["base dados"].ConnectionString;
            if (cs == null)
            {
                throw new Exception("Connection String must be configured in the config file!");
            }
        }

        public List<CompetenciaColaborador> GetPartidas()
        {
            var partidas = new List<CompetenciaColaborador>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cs))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("select * from Partida", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.Text;

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                CompetenciaColaborador p = new CompetenciaColaborador
                                {
                                    /*id = sqlDataReader.SafeGet<int>(0),
                                    tipo = sqlDataReader.SafeGet<string>(1),
                                    id1 = sqlDataReader.SafeGet<int>(2),
                                    id2 = sqlDataReader.SafeGet<int>(3),
                                    dataa = sqlDataReader.SafeGet<DateTime>(4),
                                    resultado = sqlDataReader.SafeGet<string>(5),
                                    id_estado = sqlDataReader.SafeGet<int>(6),
                                    tempo = sqlDataReader.SafeGet <TimeSpan>(7)
                                    */
                                };

                                partidas.Add(p);
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
            return partidas;
        }
    }

}
