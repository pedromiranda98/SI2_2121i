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
    class Procedure1C 
    {
        private readonly string cs;

        public Procedure1C()
        {
            cs = Session.GetConnectionString();
        }
        public void interAtribution(string desc) // a atribuição de uma equipa já se encontrava automatizada na realização do procedureE
        {
            try
            {
                using (TransactionScope ts = Transaction.Ts.GetTsReadCommitted())
                {
                    ProcedureF createInt = new ProcedureF();


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
