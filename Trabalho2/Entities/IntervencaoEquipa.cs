using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
    {
    public class IntervencaoEquipa
    {
        public int id_equipa { get; set; }
        public int id_intervencao { get; set; }
        public DateTime data_inicio { get; set; }
        public DateTime data_fim { get; set; }
    }
}
