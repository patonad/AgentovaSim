using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using OSPDataStruct;
using PropertyChanged;

namespace AgentovaSim.PomocneTriedy
{
    [AddINotifyPropertyChangedInterface]
    public class Vozidlo
    {
        public Vozidlo(int pocetDvery, int kpacita, Linka linka, string typ)
        {
            PocetDvery = pocetDvery;
            Kpacita = kpacita;
            Linka = linka;
            Typ = typ;
        }

        public bool Odchod { get; set; } = false;
        public string Typ { get; set; }
        public bool Caka { get; set; } = false;
        public Linka Linka { get; set; }
        public string Cesta { get; set; } = "ST";

        public int Percenta { get; set; }
        public double Strat { get; set; }
        public double Koniec { get; set; }
        public int PocetDvery { get; set; }
        public int  Obsadene { get; set; }
        public int Kpacita { get; set; }
        public int AktualnyPresun { get; set; }
        public int PocetObsadenychDvery { get; set; } = 0;
        public SimQueue<Cestujuci> NastupenyCestujuci { get; set; } = new SimQueue<Cestujuci>();

        public void Prerataj(double AktulaCas)
        {

            var a = (AktulaCas - Strat) / (Koniec - Strat);
            var b = (int)(a * 100);
            if (b > 100)
            {
                b = 100;
            }
            if (b < 0)
            {
                b = 0;
            }
            Percenta = b;

        }

        public void Nastup(Cestujuci ces)
        {
            NastupenyCestujuci.Enqueue(ces);
            Obsadene++;
        }

        public bool JePrazdny()
        {
            return Obsadene == 0;
        }
        public bool JePlny()
        {
            return Obsadene == Kpacita;
        }
        public Cestujuci Vystup()
        {
            Obsadene--;
            return NastupenyCestujuci.Dequeue();
        }
    }
}
