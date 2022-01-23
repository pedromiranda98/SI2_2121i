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
        public DataTable ListInterByYear(int ano)
        {
            try
            {
                DataTable table = new DataTable();
                using (TransactionScope ts = Transaction.Ts.GetTsReadCommitted())
                {
                    
                    using (SqlConnection sqlConnection = new SqlConnection(cs))
                    {
                        sqlConnection.Open();
                        using (SqlCommand sqlCommand = new SqlCommand("select dbo.ListInterByYear(@ano)", sqlConnection))
                        //using (SqlCommand sqlCommand = new SqlCommand("select * from ListInterByYear", sqlConnection))
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

