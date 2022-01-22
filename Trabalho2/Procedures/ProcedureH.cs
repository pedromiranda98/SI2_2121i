using Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Procedures
{
    public class ProcedureH
    {
        private readonly string cs;

        public ProcedureH()
        {
            cs = Session.GetConnectionString();
        }
        public void updateTeamElements(decimal id , decimal id_equipa, decimal id_competencia, int delete_or_add )
        {
            try
            {
                using (TransactionScope ts = Transaction.Ts.GetTsReadCommitted())
                {
                    using (SqlConnection sqlConnection = new SqlConnection(cs))
                    {
                        sqlConnection.Open();
                        using (SqlCommand sqlCommand = new SqlCommand("updateTeamElements", sqlConnection))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;

                            SqlParameter p1 = new SqlParameter("@id", id);
                            SqlParameter p2 = new SqlParameter("@id_equipa", id_equipa);
                            SqlParameter p3 = new SqlParameter("@id_competencia", id_competencia);
                            SqlParameter p4 = new SqlParameter("@delete_or_add", delete_or_add);


                            sqlCommand.Parameters.Add(p1);
                            sqlCommand.Parameters.Add(p2);
                            sqlCommand.Parameters.Add(p3);
                            sqlCommand.Parameters.Add(p4);

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
