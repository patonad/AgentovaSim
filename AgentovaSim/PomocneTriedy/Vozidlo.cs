using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSPDataStruct;
using PropertyChanged;

namespace AgentovaSim.PomocneTriedy
{
    [AddINotifyPropertyChangedInterface]
    public class Vozidlo
    {
        public Vozidlo(int pocetDvery, int kpacita, Linka linka)
        {
            PocetDvery = pocetDvery;
            Kpacita = kpacita;
            Linka = linka;
        }

        public Linka Linka { get; set; }    
        public string Cesta { get; set; }
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
