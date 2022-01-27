using Procedures;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SI2_Trab2.EF;
//using System.Transactions;

namespace SI2_Trab2.UI
{
    class getAvailableTeam_UI
    {
        public static int getAvailableTeam()
        {
            try
            {
               
                Console.WriteLine("Insira a descrição da intervenção :");
                string desc = Console.ReadLine();


                Console.Write("Insira 'A' caso queira o formato ADO.Net ou 'E' caso queira o formato EF: ");
                string option = Console.ReadLine();
                if (option == "A")
                {
                    ProcedureE getTeam = new ProcedureE();
                    decimal aTeam = getTeam.GetAvailableTeam(desc); 
                    Console.WriteLine("A equipa ideal para esta intervenção é " + aTeam);
                    return 0;
                }
                else if (option == "E")
                {
                    
                    return 0;
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
