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
    class createIntervention_UI
    {
        public static int createIntervention()
        {
            try
            {
               
                Console.WriteLine("Insira o id do ativo para intervenção: ");
                int ativo_id = int.Parse(Console.ReadLine());

                Console.WriteLine("Insira um valor para a intervenção: ");
                decimal valor = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Insira uma data de inicio para a intervenção no formate dd/mm/aaaa: ");
                DateTime data_inicio = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Insira uma data de fim para a intervenção no formate dd/mm/aaaa: ");
                DateTime data_fim = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Insira a periodicidade da intervenção: ");
                int periodicidade = int.Parse(Console.ReadLine());

                Console.WriteLine("Insira uma descricao para a intervenção: ");
                string descricao = Console.ReadLine();

                Console.Write("Insira 'A' caso queira o formato ADO.Net ou 'E' caso queira o formato EF: ");
                string option = Console.ReadLine();
                if (option == "A")
                {
                    ProcedureF createInt = new ProcedureF();
                    createInt.createIntervention(ativo_id, valor, data_inicio, data_fim, periodicidade, descricao);
                    Console.WriteLine("Intervenção criada com sucesso ");
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
