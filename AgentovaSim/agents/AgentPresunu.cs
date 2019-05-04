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
            GenerujVozidla();
        }

        private void GenerujVozidla()
        {
            var sim = (MySimulation)MySim;
            var parent = (AgentVozidiel)Parent;
            for (int i = 0; i < sim._linkaATyp1; i++)
            {
               // var voz = new Vozidlo(4, 186, parent.LinkaA);
                var voz = new Vozidlo(4, 186, parent.LinkaA) { AktualnyPresun = 12};
                ListVozideil.Add(voz);
                parent.LinkaA.ListVozidiel.Add(voz);
            }
            for (int i = 0; i < sim._linkaATyp2; i++)
            {
                var voz = new Vozidlo(3, 107, parent.LinkaA) {AktualnyPresun = 5};
                ListVozideil.Add(voz);
                parent.LinkaA.ListVozidiel.Add(voz);
            }
            for (int i = 0; i < sim._linkaBTyp1; i++)
            {
                var voz = new Vozidlo(4, 186, parent.LinkaB) { AktualnyPresun = 0 };
                ListVozideil.Add(voz);
                parent.LinkaB.ListVozidiel.Add(voz);
            }
            for (int i = 0; i < sim._linkaBTyp2; i++)
            {
                var voz = new Vozidlo(3, 107, parent.LinkaB) { AktualnyPresun = 5 };
                ListVozideil.Add(voz);
                parent.LinkaB.ListVozidiel.Add(voz);
            }
            for (int i = 0; i < sim._linkaCTyp1; i++)
            {
                var voz = new Vozidlo(4, 186, parent.LinkaC) { AktualnyPresun = 0 };
                ListVozideil.Add(voz);
                parent.LinkaC.ListVozidiel.Add(voz);

            }
            for (int i = 0; i < sim._linkaCTyp2; i++)
            {
                var voz = new Vozidlo(3, 107, parent.LinkaC) { AktualnyPresun = 5 };
                ListVozideil.Add(voz);
                parent.LinkaC.ListVozidiel.Add(voz);
            }
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            //MyMessage ms = new MyMessage(MySim);
            //ms.Addressee = FindAssistant(SimId.ProcesPresun); ;
            //ms.Vozidlo = ((AgentPresunu)MySim.FindAgent(SimId.AgentPresunu)).ListVozideil.FirstOrDefault();
            //MyManager.StartContinualAssistant(ms);

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