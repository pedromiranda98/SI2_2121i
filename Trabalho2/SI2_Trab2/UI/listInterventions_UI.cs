using Procedures;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SI2_Trab2.EF;
using System.Data;
using ConsoleTables;
//using System.Transactions;

namespace SI2_Trab2.UI
{
    class listInterventions_UI
    {
        public static int listInterventions()
        {
            try
            {
               
                Console.WriteLine("Insira o ano das intervenções que deseja ver: ");
                int ano = int.Parse(Console.ReadLine());

                Console.Write("Insira 'A' caso queira o formato ADO.Net ou 'E' caso queira o formato EF: ");
                string option = Console.ReadLine();
                if (option == "A")
                {
                    ProcedureI procedureI = new ProcedureI();
                    DataTable interv = procedureI.ListInterByYear(ano);

                   
                    ConsoleTable table = new ConsoleTable("Código", "Descrição");

                      foreach (DataRow dr in interv.Rows)
                       {
                            table.AddRow(dr[0], dr[1]);
                       }
                        table.Write();
                        Console.WriteLine();
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
