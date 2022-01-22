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
    public class ProcedureE
    {
        private readonly string cs;

        public ProcedureE()
        {
            cs = Session.GetConnectionString();
        }
        public decimal GetAvailableTeam(string desc)
        {
            decimal idEquipa=0;
            try
            {
                using (TransactionScope ts = Transaction.Ts.GetTsReadCommitted())
                {
                    using (SqlConnection sqlConnection = new SqlConnection(cs))
                    {
                        sqlConnection.Open();
                        using (SqlCommand sqlCommand = new SqlCommand("select dbo.getAvailableTeam(@interv_desc)", sqlConnection))
                        //using (SqlCommand sqlCommand = new SqlCommand("select * from Ativo", sqlConnection))
                        {
                            sqlCommand.CommandType = CommandType.Text;
                            SqlParameter descParam = sqlCommand.Parameters.Add(new SqlParameter("@interv_desc", desc));

                            idEquipa = (decimal)sqlCommand.ExecuteScalar();

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
            return idEquipa;
        }
    }
}

