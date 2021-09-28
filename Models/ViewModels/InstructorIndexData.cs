using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowHouse.Models.ViewModels
{
    public class InstructorIndexData
    {
        public IEnumerable<Prodavac> Prodavaci{ get; set; }
        public IEnumerable<Polozka> Polozky{ get; set; }
        public IEnumerable<Nakup> Nakupy { get; set; }
    }
}
