using Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DAL;

namespace Procedures
{
    public class ProcedureFWithoutSP
    {
        private readonly string cs;

        public ProcedureFWithoutSP()
        {
            cs = Session.GetConnectionString();
        }
        public void createIntervention(int ativo_id, decimal valor, DateTime data_inicio, DateTime data_fim, int periodicidade, string descricao)
        {
            try
            {
                using (TransactionScope ts = Transaction.Ts.GetTsReadCommitted())
                {
                    using (SqlConnection sqlConnection = new SqlConnection(cs))
                    {
                        sqlConnection.Open();
                        using (SqlCommand sqlCommand = new SqlCommand("p_criaInter", sqlConnection))
                        {
                            sqlCommand.CommandType = CommandType.Text;

                            SqlParameter p1 = sqlCommand.Parameters.Add(new SqlParameter("@ativo_id", ativo_id));
                            SqlParameter p2 = sqlCommand.Parameters.Add(new SqlParameter("@valor", valor));
                            SqlParameter p3 = sqlCommand.Parameters.Add(new SqlParameter("@data_inicio", data_inicio));
                            SqlParameter p4 = sqlCommand.Parameters.Add(new SqlParameter("@data_fim", data_fim));
                            SqlParameter p5 = sqlCommand.Parameters.Add(new SqlParameter("@periodicidade", periodicidade));
                            SqlParameter p6 = sqlCommand.Parameters.Add(new SqlParameter("@descricao", descricao));

                            sqlCommand.ExecuteNonQuery();
                        }

                        /*using (SqlCommand sqlCommand = new SqlCommand("select * from dbo.getCla(@nomeCla)", sqlConnection))
                        {
                            sqlCommand.CommandType = CommandType.Text;
                            SqlParameter nomeClaParam = sqlCommand.Parameters.Add(new SqlParameter("@nomeCla", nomeCla));
                            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                            {
                                while (sqlDataReader.Read())
                                {
                                    Cla cc = new Cla
                                    {

                                        nome = sqlDataReader.SafeGet<string>(0),
                                        pontuacao = sqlDataReader.SafeGet<decimal>(1),
                                        max_jog = sqlDataReader.SafeGet<int>(2),
                                    };
                                    
                                    clas.Add(cc);
                                }
                            }
                        }*/
                        sqlConnection.Close();
                    }
                    ts.Complete();
                }
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);

                throw exception;
            }
        }
    }
}

