using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace AgentovaSim.PomocneTriedy
{
    [AddINotifyPropertyChangedInterface]
    public class Linka
    {
        public List<Presun> Presuny { get; set; } = new List<Presun>();
        public List<Vozidlo> ListVozidiel { get; set; } =new List<Vozidlo>();

    }
}
