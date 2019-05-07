using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentovaSim.PomocneTriedy
{
    public class Cestujuci
    {
        public string Zastavka { get; set; }
        public double ZaciatokCakania { get; set; }

        public double DobaCakania(double aktualCas)
        {
            return aktualCas - ZaciatokCakania;
        }
    }
}
