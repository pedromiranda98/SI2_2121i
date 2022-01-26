using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class IntervencaoService
    {
        private IntervenctionMapper intervService = new IntervenctionMapper();
        public List<Intervencao> GetIntervencao()
        {
            return intervService.getIntervenction();
        }
    }
}
