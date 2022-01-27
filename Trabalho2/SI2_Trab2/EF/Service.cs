using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SI2_Trab2.EF
{
    class Service
    {
        internal L51NG7Entities context;

        public Service(L51NG7Entities context)
        {
            this.context = context;

        }

        public void SaveChanges()
        {
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while saving context changes to database:");
                Console.WriteLine("-- {0}", e.GetBaseException().Message);
            }
        }

        public void RefreshAll()
        {
            foreach (var entity in context.ChangeTracker.Entries())
            {
                entity.Reload();
            }
        }



        public void ResetDatabase()
        {
            try
            {
                context.drop_tables();
                context.CreateTables();
                context.PopulateTables();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while reseting database:");
                throw e.GetBaseException();
            }
        }

        public void createIntervention(int ativo_id, decimal valor, DateTime data_inicio, DateTime data_fim, int periodicidade, string descricao)
        {
            try
            {
                context.p_criaInter(ativo_id, valor, data_inicio, data_fim, periodicidade, descricao);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error while creating new intervention:");
                throw e.GetBaseException();
            }
        }

        public void addNewTeam(string localizacao)
        {
            try
            {
                context.addNewTeam(localizacao);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error while adding team:");
                throw e.GetBaseException();
            }
        }

        public void updateTeamElements(decimal id, decimal id_equipa, decimal id_competencia, int delete_or_add)
        {
            try
            {
                context.updateTeamElements(id, id_equipa, id_competencia, delete_or_add);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error while updating team:");
                throw e.GetBaseException();
            }
        }
    }
}
