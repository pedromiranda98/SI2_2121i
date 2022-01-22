using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Intervencao
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public string estado { get; set; }
        public decimal valor { get; set; }
        public DateTime data_inicio { get; set; }
        public DateTime data_fim { get; set; }
        public int periodicidade { get; set; }
        public int ativo_id { get; set; }



    }
}
