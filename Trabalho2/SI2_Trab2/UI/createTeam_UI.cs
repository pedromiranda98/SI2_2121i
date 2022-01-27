using Procedures;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SI2_Trab2.EF;
using System.Transactions;
//using System.Transactions;

namespace SI2_Trab2.UI
{
    class createTeam_UI
    {
        public static int createTeam()
        {
            try
            {
               
                Console.WriteLine("Insira a localização da equipa :");
                string local = Console.ReadLine();


                Console.Write("Insira 'A' caso queira o formato ADO.Net ou 'E' caso queira o formato EF: ");
                string option = Console.ReadLine();
                if (option == "A")
                {
                    ProcedureG createTeam= new ProcedureG();
                    createTeam.addNewTeam(local);
                    Console.WriteLine("Equipa criada com sucesso");
                    return 0;
                }
                else if (option == "E")
                {

                    using (TransactionScope ts = Transaction.Ts.GetTsReadCommitted())
                    {
                        using (EF.L51NG7Entities context = new EF.L51NG7Entities())
                        {
                            Service service = new Service(context);
                            service.addNewTeam(local);
                            service.SaveChanges();
                        }
                        ts.Complete();
                        return 0;
                    }
                }
                else { Console.WriteLine("Invalid Option"); return -1; }
            }
            catch (Exception e)
            {
                Console.WriteLine("Valores Inseridos não são validos " + e.Message);
                return -1;
            }
        }
    }
}
