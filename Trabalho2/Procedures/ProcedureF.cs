using Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Procedures
{
    public class ProcedureF
    {
        private readonly string cs;

        public ProcedureF()
        {
            cs = Session.GetConnectionString();
        }
        public void createIntervention(int ativo_id , decimal valor, DateTime data_inicio, DateTime data_fim, int periodicidade, string descricao)
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
                            sqlCommand.CommandType = CommandType.StoredProcedure;

                            SqlParameter p1 = new SqlParameter("@ativo_id", ativo_id);
                            SqlParameter p2 = new SqlParameter("@valor", valor);
                            SqlParameter p3 = new SqlParameter("@data_inicio", data_inicio);
                            SqlParameter p4 = new SqlParameter("@data_fim", data_fim);
                            SqlParameter p5 = new SqlParameter("@periodicidade", periodicidade);
                            SqlParameter p6 = new SqlParameter("@descricao", descricao);



                            sqlCommand.Parameters.Add(p1);
                            sqlCommand.Parameters.Add(p2);
                            sqlCommand.Parameters.Add(p3);
                            sqlCommand.Parameters.Add(p4);
                            sqlCommand.Parameters.Add(p5);
                            sqlCommand.Parameters.Add(p6);

                            sqlCommand.ExecuteNonQuery();
                        }
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
