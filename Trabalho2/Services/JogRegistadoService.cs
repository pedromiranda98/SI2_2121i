using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class JogRegistadoService
    {
        private JogRegistadoMapper jogRegService = new JogRegistadoMapper();
        public List<ColaboradorEquipa> GetJogRegistados()
        {
            return jogRegService.GetJog_registados();
        }

        public void UpdateJogadorCla(string username, string cla)
        {
            
            jogRegService.UpdateJogadorCla(username, cla);
        }
    }
}
