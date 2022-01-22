using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PartidaService
    {
        private PartidaMapper partidaService = new PartidaMapper();
        public List<CompetenciaColaborador> GetPartidas()
        {
            return partidaService.GetPartidas();
        }
    }
}
