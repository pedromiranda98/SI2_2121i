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

namespace Procedures
{
    public class ProcedureI
    {
        private readonly string cs;

        public ProcedureI()
        {
            cs = Session.GetConnectionString();
        }
        public DataTable InfoJogador()
        {
            //var jogadores = new List<Jog_registado>();
            try
            {
                DataTable table = new DataTable();
                using (TransactionScope ts = Transaction.Ts.GetTsReadCommitted())
                {
                    
                    using (SqlConnection sqlConnection = new SqlConnection(cs))
                    {
                        sqlConnection.Open();
                        using (SqlCommand sqlCommand = new SqlCommand("select * from InfoJogador", sqlConnection))
                        {
                            table.Load(sqlCommand.ExecuteReader());
                        }
                        
                        sqlConnection.Close();
                    }
                    ts.Complete();
                }
                return table;
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);

                throw exception;
            }
        }
    }
}

