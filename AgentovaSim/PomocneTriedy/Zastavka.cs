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
        public string Nazov { get; set; }
        public int PocetCestujucich { get; set; }
       
        public Zastavka(string nazov,int maxCes)
        {
            Nazov = nazov;
            PocetCestujucich = 0;
        }
    }
}
