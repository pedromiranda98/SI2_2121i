using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Equipa
    {
        public int id { get; set; }
        public string localizacao { get; set; }
        public int n_elemento { get; set; }
        public int intervencoes_atribuidas { get; set; }
    }
}
