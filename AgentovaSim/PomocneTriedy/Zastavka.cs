﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSPDataStruct;
using OSPRNG;
using PropertyChanged;

namespace AgentovaSim.PomocneTriedy
{
    [AddINotifyPropertyChangedInterface]
    public class Zastavka
    {
        public string Nazov { get; set; }
        public int PocetCestujucich { get; set; }
        private SimQueue<Cestujuci> _cestujuciQueue{ get; set; } = new SimQueue<Cestujuci>();
        public Vozidlo CakajuceVozidlo { get; set; }
        public Zastavka(string nazov)
        {
            Nazov = nazov;
            PocetCestujucich = 0;
            //for (int i = 0; i < 10; i++)
            //{
            //    Enqueue(new Cestujuci(){ZaciatokCakania = 0});
            //}
        }

        public Cestujuci Peek()
        {
            return _cestujuciQueue.Peek();
        }

        public void Enqueue(Cestujuci ces)
        {
            _cestujuciQueue.Enqueue(ces);
            PocetCestujucich += 1;
        }
        public Cestujuci Dequeue()
        {
            PocetCestujucich -= 1;
            return _cestujuciQueue.Dequeue();
        }
    }
}
