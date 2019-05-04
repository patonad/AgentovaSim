using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace AgentovaSim.PomocneTriedy
{
    [AddINotifyPropertyChangedInterface]
    public class Presun
    {
        public Zastavka ZastavkaStart { get; set; }
        public Zastavka ZastavkaKoniec { get; set; }
        public double CasPresunu { get; set; }

        public Presun(Zastavka zastavkaStart, Zastavka zastavkaKoniec, double casPresunu)
        {
            ZastavkaStart = zastavkaStart;
            ZastavkaKoniec = zastavkaKoniec;
            CasPresunu = casPresunu;
        }
        public override string ToString()
        {
            return ZastavkaStart.Nazov + " " + ZastavkaKoniec.Nazov + " " + CasPresunu;
        }
    }
}
