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
           NONE,
           Exit,
           getAvailableTeam,
           criaInter,
           createTeam,
           addOrRemoveTeamElement,
           listIntervFromYear,
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
            funcs = new Dictionary<OPTIONS, DBMethod>();
            funcs.Add(OPTIONS.getAvailableTeam, UI.getAvailableTeam_UI.getAvailableTeam);
            funcs.Add(OPTIONS.criaInter, UI.createIntervention_UI.createIntervention);
            funcs.Add(OPTIONS.createTeam, UI.createTeam_UI.createTeam);
            funcs.Add(OPTIONS.addOrRemoveTeamElement, UI.updateTeamElements_UI.updateTeamElements);
            funcs.Add(OPTIONS.listIntervFromYear, UI.listInterventions_UI.listInterventions);
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
            OPTIONS userInput = OPTIONS.NONE;
            do
            {
                Console.Clear();
                userInput = DisplayMenu();
                Console.Clear();
                try
                {
                    funcs[userInput]();
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                }
                catch (KeyNotFoundException ex)
                {
                    //Nothing to do. The option was not a valid one. Read another.
                }

            } while (userInput != OPTIONS.Exit);
        }


    }

        class MainClass
        {
            public static void Main(String[] args)
            {
                App.Instance.Run();
            }
        }
    }