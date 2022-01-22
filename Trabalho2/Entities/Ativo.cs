using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Ativo
    {
      
        public int id { get; set; }
        public int parent_id { get; set; }
        public string nome { get; set; }
        public DateTime data_aquisicao { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }
        public string localizacao { get; set; }
        public int estado { get; set; }
        public int id_tipo { get; set; }

    }
}
