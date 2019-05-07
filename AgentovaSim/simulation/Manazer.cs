using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PropertyChanged;
using simulation;

namespace AgentovaSim.simulation
{
    [AddINotifyPropertyChangedInterface]

    public class Manazer
    {
        public MySimulation MySimulation { get; set; }
        public bool start { get; set; }
        public bool pouse { get; set; }
        public bool stop { get; set; }
        public bool Fast { get; set; }
        public int _linkaATyp1;
        public int _linkaATyp2;
        public int _linkaAMicro;
        public int _linkaBTyp1;
        public int _linkaBTyp2;
        public int _linkaBMicro;
        public int _linkaCTyp1;
        public int _linkaCTyp2;
        public int _linkaCMicro;
        public DateTime CurrentDateTime { get; set; } = new DateTime(1,1,1,12,0,0);
        public bool _cakanie;

        public int _pocet;
        private Thread _thread;

        public Manazer()
        {
            start = false;
            pouse = false;
            stop = true;
           
        }
        public void Stop()
        {
            if (!stop)
            {
                if (_thread.ThreadState == ThreadState.Suspended)
                {
                    _thread.Resume();
                }

                _thread.Abort();
                start = false;
                stop = true;
                pouse = false;
            }
        }

        public void Pause()
        {
            if (start && !stop && !pouse)
            {
                _thread.Suspend();
                start = false;
                pouse = true;
            }
        }

        public void Resume()
        {
            if (!start && !stop && pouse)
            {

                _thread.Resume();
                pouse = false;
                start = true;

            }
        }

        public void Simuluj()
        {
            MySimulation.Manazer = this;
            MySimulation.CasZapasu = CurrentDateTime.TimeOfDay;
            MySimulation.Simulate(_pocet);
        }
        public void SpustiSimulaciuRychlo()
        {
            if (!start && stop && !pouse)
            {
                MySimulation = new MySimulation(_linkaATyp1, _linkaATyp2, _linkaAMicro, _linkaBTyp1, _linkaBTyp2, _linkaBMicro, _linkaCTyp1, _linkaCTyp2, _linkaCMicro);
                MySimulation._cakanie = _cakanie;
                MySimulation.Fast = true;
                _thread = new Thread(Simuluj);
                _thread.Start();
                start = true;
                stop = false;

            }
        }
        public void SpustiSimulaciuPomala()
        {
            if (!start && stop && !pouse)
            {
                MySimulation = new MySimulation(_linkaATyp1,_linkaATyp2,_linkaAMicro, _linkaBTyp1, _linkaBTyp2, _linkaBMicro, _linkaCTyp1, _linkaCTyp2, _linkaCMicro);
                MySimulation._cakanie = _cakanie;
                _pocet = 1;
                _thread = new Thread(Simuluj);
                _thread.Start();
                start = true;
                stop = false;

            }
        }
    }
}
