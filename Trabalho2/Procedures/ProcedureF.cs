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
        public void createPartida(string tipo, int id1, int id2)
        {
            try
            {
                using (TransactionScope ts = Transaction.Ts.GetTsReadCommitted())
                {
                    using (SqlConnection sqlConnection = new SqlConnection(cs))
                    {
                        sqlConnection.Open();
                        using (SqlCommand sqlCommand = new SqlCommand("createPartida", sqlConnection))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;

                            SqlParameter p1 = new SqlParameter("@tipo", tipo);
                            SqlParameter p2 = new SqlParameter("@id1", id1);
                            SqlParameter p3 = new SqlParameter("@id2", id2);
                           

                            sqlCommand.Parameters.Add(p1);
                            sqlCommand.Parameters.Add(p2);
                            sqlCommand.Parameters.Add(p3);

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
