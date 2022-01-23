using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace Services
{
    public class EquipaService
    {
        private EquipaMapper equipaMapper = new EquipaMapper();
        public List<Equipa> GetEquipa()
        {
            return equipaMapper.GetEquipa();
        }

    }
}
