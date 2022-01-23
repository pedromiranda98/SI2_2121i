using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using Procedures;

namespace SI2_Trab2
{
    class App
    {
        public enum OPTIONS
        {
           NONE
        };

        private delegate int DBMethod();
        private Dictionary<OPTIONS, DBMethod> funcs;
        private static App _instance;
        public static App Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new App();
                return _instance;
            }
            private set { }
        }

        private App()
        {

        }

        public OPTIONS DisplayMenu()
        {

            OPTIONS option = OPTIONS.NONE;
            try
            {
                Array arr = Enum.GetValues(typeof(OPTIONS));
                Console.WriteLine("Choose an option");
                for (int i = 1; i < arr.Length; i++)
                {
                    Console.WriteLine("{0} {1}. ", i, arr.GetValue(i).ToString());
                }
                Console.WriteLine();
                Console.Write("-> ");
                var result = Console.ReadLine();
                option = (OPTIONS)Enum.Parse(typeof(OPTIONS), result);
            }
            catch (ArgumentException ex)
            {
                //nothing to do. User press select no option and press enter.
            }
            return option;
        }

        public void Run()
        {
            new InicialProcedure().ResetDB();

            ProcedureE ProdE = new ProcedureE();
            ProcedureF ProdF = new ProcedureF();
            ProcedureFWithoutSP ProdFWSP = new ProcedureFWithoutSP();
            ProcedureG ProdG = new ProcedureG();
            ProcedureH ProdH = new ProcedureH();
            ProcedureI ProdI = new ProcedureI();

            //Console.WriteLine(ProdE.GetAvailableTeam("ola"));
            //ProdF.createIntervention(000000001, (decimal)28.10, Convert.ToDateTime("2000-11-10"), Convert.ToDateTime("2000-11-15"), 5, "avaria");
            ProdFWSP.createIntervention(000000001, (decimal)28.10, Convert.ToDateTime("2000-11-10"), Convert.ToDateTime("2000-11-15"), 5, "avaria");
            //ProdG.addNewTeam("Moita");
            //ProdH.updateTeamElements(111222555, 30000, 123, 0);
            //ProdH.updateTeamElements(111222555, 30000, 123, 1);
            //Console.WriteLine(ProdI.ListInterByYear(Convert.ToDateTime("2017-01-01").Year));


        }

        class MainClass
        {
            public static void Main(String[] args)
            {
                App.Instance.Run();
            }
        }
    }
}