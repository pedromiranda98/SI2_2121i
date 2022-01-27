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
    class updateTeamElements_UI
    {
        public static int updateTeamElements()
        {
            try
            {
                Console.WriteLine("Deseja remover ou adicionar o funcionário?: ");
                Console.WriteLine("0 - Apagar ");
                Console.WriteLine("1 - Adiciona: ");
                Console.WriteLine("");


                int decisao = int.Parse(Console.ReadLine());
                string insertOrDelete = "";

                if(decisao!= 1 && decisao != 0)
                {
                    Console.WriteLine("Decisão inválida");
                    return -1;
                }

                insertOrDelete = decisao == 0 ?  "Funcionario removido com sucesso" :  "Funcionario adicionado com sucesso";

                Console.WriteLine("Insira o id do funcionario: ");
                decimal func_id = int.Parse(Console.ReadLine());

                Console.WriteLine("Insira o id da equipa: ");
                decimal id_equipa = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Insira o id da competencia do funcionario na equipa: ");
                decimal competencia = decimal.Parse(Console.ReadLine());


                Console.Write("Insira 'A' caso queira o formato ADO.Net ou 'E' caso queira o formato EF: ");
                string option = Console.ReadLine();
                if (option == "A")
                {
                    ProcedureH inRemTM = new ProcedureH();
                    inRemTM.updateTeamElements(func_id, id_equipa, competencia, decisao);
                    Console.WriteLine(insertOrDelete);
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
