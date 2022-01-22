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
        public int GetAvailableTeam(string desc)
        {
            var idEquipa=0;
            try
            {
                using (TransactionScope ts = Transaction.Ts.GetTsReadCommitted())
                {
                    using (SqlConnection sqlConnection = new SqlConnection(cs))
                    {
                        sqlConnection.Open();
                        using (SqlCommand sqlCommand = new SqlCommand("select * from dbo.getAvailableTeam(@interv_desc)", sqlConnection))
                        {
                            sqlCommand.CommandType = CommandType.Text;
                            SqlParameter nomeClaParam = sqlCommand.Parameters.Add(new SqlParameter("@interv_desc", desc));
                            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                            {
                                while (sqlDataReader.Read())
                                {
                                    idEquipa = sqlDataReader.SafeGet<int>(0);
                                }
                            }
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

