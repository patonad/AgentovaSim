using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Web.UI.DataVisualization.Charting;

namespace AgentovaSim.PomocneTriedy
{
    class IntervalSpolahlivosti
    {
        public double LavaStrana { get; set; } = 0;
        public double PravaStrana { get; set; } = 0;
        private double _sucetNa2;
        private double _sucet;
        private int _count;

        public void add(double cislo)
        {
            _count++;
            _sucetNa2 += cislo * cislo;
            _sucet += cislo;


        }

        public void Prerataj()
        {
            //var s = Math.Sqrt(_sucetNa2 / _count - Math.Pow(_sucet / _count, 2));
            //var priemer = _sucet / _count;
            //if (_count > 30)
            //{
            //   // double t = new Chart().DataManipulator.Statistics.InverseTDistribution(0.9, _count - 1);
            //    LavaStrana = priemer - s * t / Math.Sqrt(_count - 1);
            //    PravaStrana = priemer + t * s / Math.Sqrt(_count - 1);
            //}
        }
    }
}
