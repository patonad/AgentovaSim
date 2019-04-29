using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSPRNG;

namespace AgentovaSim.PomocneTriedy
{
    public class Zastavka
    {
        public ExponentialRNG ExponentialRng { get; set; }
        public string Nazov { get; set; }
        public int PocetCestujucich { get; set; }
        public int MaxCestujucich { get; set; }

        public Zastavka(string nazov,int maxCes)
        {
            Nazov = nazov;
            ExponentialRng = new ExponentialRNG((65.0/maxCes)*60);
            PocetCestujucich = 0;
            MaxCestujucich = maxCes;

        }
    }
}
