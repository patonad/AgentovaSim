using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Shapes;
using AgentovaSim.continualAssistants;
using AgentovaSim.PomocneTriedy;
using OSPABA;
using simulation;
using managers;
using continualAssistants;
using PropertyChanged;

namespace agents
{
    [AddINotifyPropertyChangedInterface]
    //meta! id="3"
    public class AgentVozidiel : Agent
    {
        public Linka LinkaA { get; set; } = new Linka();
        public Linka LinkaB { get; set; } = new Linka();
        public Linka LinkaC { get; set; } = new Linka();
        public AgentVozidiel(int id, Simulation mySim, Agent parent) :
            base(id, mySim, parent)
        {
            Init();
        }

        private void PustiVozidla(List<Vozidlo> voz1, List<Vozidlo> voz2, List<Vozidlo> vozM)
        {
            var count = voz1.Count + voz2.Count;
            if (count == 0)
                return;

              var cas = 1800 / (count);
            //var cas =240;
            int a = 0;
            while (voz1.Count != 0 || voz2.Count != 0 || vozM.Count != 0)
            {
                if (voz1.Count != 0)
                {
                    var vozidlo = voz1.First();
                    voz1.RemoveAt(0);
                    MyMessage ms = new MyMessage(MySim);
                    ms.Vozidlo = vozidlo;
                    ms.Addressee = FindAssistant(SimId.ProcesSpustiVozidla);
                    ms.Oneskorenie = cas * a;
                    MyManager.StartContinualAssistant(ms);
                    a++;
                }
                if (voz2.Count != 0)
                {
                    var vozidlo = voz2.First();
                    voz2.RemoveAt(0);
                    MyMessage ms = new MyMessage(MySim);
                    ms.Vozidlo = vozidlo;
                    ms.Addressee = FindAssistant(SimId.ProcesSpustiVozidla);
                    ms.Oneskorenie = cas * a;
                    MyManager.StartContinualAssistant(ms);
                    a++;
                }
                if (vozM.Count != 0)
                {
                    var vozidlo = vozM.First();
                    vozM.RemoveAt(0);
                    MyMessage ms = new MyMessage(MySim);
                    ms.Vozidlo = vozidlo;
                    ms.Addressee = FindAssistant(SimId.ProcesSpustiVozidla);
                    ms.Oneskorenie = cas * a;
                    MyManager.StartContinualAssistant(ms);
                }
                a++;
                
            }
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            var voz1 = LinkaA.ListVozidiel.Where(x => x.Typ == "A" && x.PocetDvery == 4).ToList();
            var voz2 = LinkaA.ListVozidiel.Where(x => x.Typ == "A" && x.PocetDvery == 3).ToList();
            var vozM = LinkaA.ListVozidiel.Where(x => x.Typ == "M").ToList();
            PustiVozidla(voz1, voz2, vozM);

            voz1 = LinkaB.ListVozidiel.Where(x => x.Typ == "A" && x.PocetDvery == 4).ToList();
            voz2 = LinkaB.ListVozidiel.Where(x => x.Typ == "A" && x.PocetDvery == 3).ToList();
            vozM = LinkaB.ListVozidiel.Where(x => x.Typ == "M").ToList();
            PustiVozidla(voz1, voz2, vozM);

            voz1 = LinkaC.ListVozidiel.Where(x => x.Typ == "A" && x.PocetDvery == 4).ToList();
            voz2 = LinkaC.ListVozidiel.Where(x => x.Typ == "A" && x.PocetDvery == 3).ToList();
            vozM = LinkaC.ListVozidiel.Where(x => x.Typ == "M").ToList();
            PustiVozidla(voz1, voz2, vozM);
            LinkaA.VynulujSa();
            LinkaB.VynulujSa();
            LinkaC.VynulujSa();
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        private void Init()
        {
            new ManagerVozidiel(SimId.ManagerVozidiel, MySim, this);
            new ProcesSpustiVozidla(SimId.ProcesSpustiVozidla, MySim, this);
            AddOwnMessage(Mc.Presun);
            AddOwnMessage(Mc.PrichodCestuVozidlo);
            AddOwnMessage(Mc.Nastupenie);
            AddOwnMessage(Mc.PrichodVozidla);
        }
        //meta! tag="end"
    }
}