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
    public class ProcedureH
    {
        private readonly string cs;

        public ProcedureH()
        {
            cs = Session.GetConnectionString();
        }
        public void createPlayer(string nomeJog, string item, string tipoJog, string usernameJog, string senhaJog, DateTime dt_nascimentoJog,decimal pontuacaoJog , int nivelJog, string cla)
        {
            try
            {
                using (TransactionScope ts = Transaction.Ts.GetTsReadCommitted())
                {
                    using (SqlConnection sqlConnection = new SqlConnection(cs))
                    {
                        sqlConnection.Open();
                        using (SqlCommand sqlCommand = new SqlCommand("createPlayer", sqlConnection))
                        {
                            sqlCommand.CommandType = CommandType.StoredProcedure;

                            SqlParameter p1 = new SqlParameter("@nomeJog", nomeJog);
                            SqlParameter p2 = new SqlParameter("@item", item);
                            SqlParameter p3 = new SqlParameter("@tipoJog", tipoJog);
                            SqlParameter p4 = new SqlParameter("@usernameJog", usernameJog);
                            SqlParameter p5 = new SqlParameter("@senhaJog", senhaJog);
                            SqlParameter p6 = new SqlParameter("@dt_nascimentoJog", dt_nascimentoJog);
                            SqlParameter p7 = new SqlParameter("@pontuacaoJog", pontuacaoJog);
                            SqlParameter p8 = new SqlParameter("@nivelJog", nivelJog);
                            SqlParameter p9 = new SqlParameter("@cla", cla);


                            sqlCommand.Parameters.Add(p1);
                            sqlCommand.Parameters.Add(p2);
                            sqlCommand.Parameters.Add(p3);
                            sqlCommand.Parameters.Add(p4);
                            sqlCommand.Parameters.Add(p5);
                            sqlCommand.Parameters.Add(p6);
                            sqlCommand.Parameters.Add(p7);
                            sqlCommand.Parameters.Add(p8);
                            sqlCommand.Parameters.Add(p9);

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
