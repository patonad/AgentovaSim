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
        public double PrCasCakana { get; set; }
        public double SucetCasCakana { get; set; } = 0;
        public int Nastupeny { get; set; } = 0;
        public int PoZaciatku { get; set; } = 0;
        public double PoZaciatkuPomer { get; set; } = 0;
        public List<Presun> Presuny { get; set; } = new List<Presun>();
        public List<Vozidlo> ListVozidiel { get; set; } =new List<Vozidlo>();

        public void VynulujSa()
        {
            PrCasCakana = 0;
            SucetCasCakana = 0;
            Nastupeny = 0;
            PoZaciatku = 0;
            PoZaciatkuPomer = 0;
        }
    }
}
