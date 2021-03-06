﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSPRNG;

namespace AgentovaSim.simulation
{
    public class GeneratorCestujucych
    {
        public GeneratorCestujucych(double mean, string nazov, double oneskorenie, int kolko, Random seed)
        {
            ExponentialRng = new ExponentialRNG(mean,seed);
            Nazov = nazov;
            Oneskorenie = oneskorenie;
            Kolko = kolko;
        }

        public bool GenerujeSa { get; set; } = true;
        public ExponentialRNG ExponentialRng { get; set; }
        public string Nazov { get; set; }
        public double Oneskorenie { get; set; }
        public double MaxCasGen { get; set; }
        public int Kolko { get; set; }
    }
}
