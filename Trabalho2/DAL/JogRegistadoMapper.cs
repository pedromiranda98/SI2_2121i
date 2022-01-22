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
    public class JogRegistadoMapper
    {
        private String cs;
        public JogRegistadoMapper()
        {
            cs = ConfigurationManager.ConnectionStrings["base dados"].ConnectionString;
            if (cs == null)
            {
                throw new Exception("Connection String must be configured in the config file!");
            }
        }

        public void UpdateJogadorCla(string username, string cla)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cs))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("update JOG_REGISTADO set nome_cla = @cla where username = @usernameJog", sqlConnection))
                    {
                        SqlParameter jogRegCla = sqlCommand.Parameters.Add(new SqlParameter("@cla", cla));
                        SqlParameter jogRegUser = sqlCommand.Parameters.Add(new SqlParameter("@usernameJog", username));
                        sqlCommand.ExecuteNonQuery();
         
                    }
                }
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw exception;
            }
        }

          public List<ColaboradorEquipa> GetJog_registados()
          {
            var jog_Registados = new List<ColaboradorEquipa>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(cs))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand("select * from JOG_REGISTADO", sqlConnection))
                    {
                        sqlCommand.CommandType = CommandType.Text;

                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                ColaboradorEquipa jr = new ColaboradorEquipa
                                {
                                    /*id_jog = sqlDataReader.SafeGet<int>(0),
                                    username = sqlDataReader.SafeGet<string>(1),
                                    senha = sqlDataReader.SafeGet<string>(2),
                                    dt_nascimento = sqlDataReader.SafeGet<DateTime>(3),
                                    pontuacao = sqlDataReader.SafeGet<decimal>(4),
                                    nivel = sqlDataReader.SafeGet<int>(5),
                                    dinheiro = sqlDataReader.SafeGet<decimal>(6),
                                    vida = sqlDataReader.SafeGet<int>(7),
                                    forca = sqlDataReader.SafeGet<int>(8),
                                    velocidade = sqlDataReader.SafeGet<int>(9),
                                    nome_cla = sqlDataReader.SafeGet<string>(10),
                                    tipo = sqlDataReader.SafeGet<string>(11)*/
                                };

                                jog_Registados.Add(jr);
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
            return jog_Registados;
        }
    }
}

