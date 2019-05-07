using System.Collections.Generic;
using System.Linq;
using AgentovaSim.PomocneTriedy;
using OSPABA;
using simulation;
using managers;
using continualAssistants;
using PropertyChanged;

namespace agents
{
    [AddINotifyPropertyChangedInterface]
    //meta! id="4"
    public class AgentPresunu : Agent
    {
        public List<Vozidlo> ListVozideil { get; set; } = new List<Vozidlo>();
        public AgentPresunu(int id, Simulation mySim, Agent parent) :
            base(id, mySim, parent)
        {
            Init();
        }

        private void GenerujVozidla()
        {
            
            var sim = (MySimulation)MySim;
            var parent = (AgentVozidiel)Parent;
            parent.LinkaA.ListVozidiel.Clear();
            parent.LinkaB.ListVozidiel.Clear();
            parent.LinkaC.ListVozidiel.Clear();
            for (int i = 0; i < sim._linkaATyp1; i++)
            {
               // var voz = new Vozidlo(4, 186, parent.LinkaA);
                var voz = new Vozidlo(4, 186, parent.LinkaA,"A") { AktualnyPresun = 14};
                ListVozideil.Add(voz);
                parent.LinkaA.ListVozidiel.Add(voz);
            }
            for (int i = 0; i < sim._linkaATyp2; i++)
            {
                var voz = new Vozidlo(3, 107, parent.LinkaA,"A") {AktualnyPresun = 14};
                ListVozideil.Add(voz);
                parent.LinkaA.ListVozidiel.Add(voz);
            }
            for (int i = 0; i < sim._linkaAMicro; i++)
            {
                var voz = new Vozidlo(1, 8, parent.LinkaA, "M") { AktualnyPresun = 14 };
                ListVozideil.Add(voz);
                parent.LinkaA.ListVozidiel.Add(voz);
            }





            for (int i = 0; i < sim._linkaBTyp1; i++)
            {
                var voz = new Vozidlo(4, 186, parent.LinkaB, "A") { AktualnyPresun = 12 };
                ListVozideil.Add(voz);
                parent.LinkaB.ListVozidiel.Add(voz);
            }
            for (int i = 0; i < sim._linkaBTyp2; i++)
            {
                var voz = new Vozidlo(3, 107, parent.LinkaB, "A") { AktualnyPresun = 12 };
                ListVozideil.Add(voz);
                parent.LinkaB.ListVozidiel.Add(voz);
            }
            for (int i = 0; i < sim._linkaBMicro; i++)
            {
                var voz = new Vozidlo(1, 8, parent.LinkaB, "M") { AktualnyPresun = 12 };
                ListVozideil.Add(voz);
                parent.LinkaB.ListVozidiel.Add(voz);
            }




            for (int i = 0; i < sim._linkaCTyp1; i++)
            {
                var voz = new Vozidlo(4, 186, parent.LinkaC, "A") { AktualnyPresun = 9 };
                ListVozideil.Add(voz);
                parent.LinkaC.ListVozidiel.Add(voz);

            }
            for (int i = 0; i < sim._linkaCTyp2; i++)
            {
                var voz = new Vozidlo(3, 107, parent.LinkaC, "A") { AktualnyPresun = 9 };
                ListVozideil.Add(voz);
                parent.LinkaC.ListVozidiel.Add(voz);
            }
            for (int i = 0; i < sim._linkaCMicro; i++)
            {
                var voz = new Vozidlo(1, 8, parent.LinkaC, "M") { AktualnyPresun = 9 };
                ListVozideil.Add(voz);
                parent.LinkaC.ListVozidiel.Add(voz);
            }

        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            GenerujVozidla();

        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        private void Init()
        {
            new ManagerPresunu(SimId.ManagerPresunu, MySim, this);
            new ProcesPresun(SimId.ProcesPresun, MySim, this);
            AddOwnMessage(Mc.Presun);
            AddOwnMessage(Mc.KoniecPresunu);
        }
        //meta! tag="end"
    }
}