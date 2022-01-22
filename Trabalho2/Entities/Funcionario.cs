using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Funcionario
    {   
        public int id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public int telemovel { get; set; }
        public string endereco { get; set; }
        public string profissao { get; set; }
        public DateTime data_nascimento { get; set; }

    }
}
