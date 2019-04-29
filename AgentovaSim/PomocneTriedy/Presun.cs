using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentovaSim.PomocneTriedy
{
    public class Presun
    {
        public Zastavka Zastavka { get; set; }
        public double CasPresunu { get; set; }

        public Presun(Zastavka zastavka, double casPresunu)
        {
            Zastavka = zastavka;
            CasPresunu = casPresunu;
        }
    }
}
