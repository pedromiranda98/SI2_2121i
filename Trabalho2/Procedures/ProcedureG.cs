using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Procedures
{
   public class ProcedureG
    {
        private readonly string cs;

        public ProcedureG()
        {
            cs = Session.GetConnectionString();
        }
        public void AddItem(int id_jogador, string nome_item)
        {
            try
            {
                using (TransactionScope ts = Transaction.Ts.GetTsReadCommitted())
                {
                    using (SqlConnection sqlConnection = new SqlConnection(cs))
                    {
                        sqlConnection.Open();
                        using (SqlCommand sqlCommand = new SqlCommand("AddItem", sqlConnection))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;

                            SqlParameter p1 = new SqlParameter("@id_jogador", id_jogador);
                            SqlParameter p2 = new SqlParameter("@nome_item", nome_item);


                            sqlCommand.Parameters.Add(p1);
                            sqlCommand.Parameters.Add(p2);

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

