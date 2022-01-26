using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ItemJogService
    {
        private AtivoMapper ativoServ = new AtivoMapper();
        public List<Ativo> getAtivos()
        {
            return ativoServ.GetAtivos();
        }
    }
}
