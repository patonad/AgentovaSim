using System.Linq;
using OSPABA;
using simulation;
using agents;
using continualAssistants;
using PropertyChanged;

namespace managers
{
    //meta! id="3"

    [AddINotifyPropertyChangedInterface]
    public class ManagerVozidiel : Manager
    {
        public ManagerVozidiel(int id, Simulation mySim, Agent myAgent) :
            base(id, mySim, myAgent)
        {
            Init();
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication

            if (PetriNet != null)
            {
                PetriNet.Clear();
            }
        }

        //meta! sender="AgentPresunu", id="10", type="Response"
        public void ProcessPresun(MessageForm message)
        {
            MyMessage ms = (MyMessage)message.CreateCopy();
            ms.Addressee = MySim.FindAgent(SimId.AgentZasrtavok);
            var vozidlo = ms.Vozidlo;
            var zastavka = vozidlo.Linka.Presuny[ms.Vozidlo.AktualnyPresun].ZastavkaStart;
            vozidlo.Cesta = zastavka.Nazov;
            ms.Code = Mc.Nastupenie;
            Notice(ms);

        }

        //meta! userInfo="Removed from model"
        public void ProcessFinish(MessageForm message)
        {
        }

        //meta! userInfo="Removed from model"
        public void ProcessNastupenie(MessageForm message)
        {
            MyMessage ms = (MyMessage)message;
            if (ms.Vozidlo.AktualnyPresun != ms.Vozidlo.Linka.Presuny.Count - 1)
            {
                ms.Addressee = MySim.FindAgent(SimId.AgentPresunu);
                ms.Code = Mc.Presun;
                Request(ms);
            }
            else
            {
                    if (ms.Vozidlo.Linka.Presuny.Any(x => x.ZastavkaStart.PocetCestujucich != 0 || ((MySimulation)MySim).GenerujeSa))
                    {
                        ms.Addressee = MySim.FindAgent(SimId.AgentPresunu);
                        ms.Code = Mc.Presun;
                        Request(ms);
                    }
                    else
                    {
                        ((MySimulation) MySim).PocetAut--;
                    }
                }
        }

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
            }
        }

        //meta! sender="AgentModelu", id="40", type="Notice"
        public void ProcessPrichodCestuVozidlo(MessageForm message)
        {
            var ms = (MyMessage)message.CreateCopy();
            ms.Addressee = MySim.FindAgent(SimId.AgentZasrtavok);
            ms.Code = Mc.PrichodCestuZastavka;
            Notice(ms);
        }

        //meta! sender="AgentZasrtavok", id="30", type="Response"


        //meta! userInfo="Generated code: do not modify", tag="begin"
        public void Init()
        {
        }

        override public void ProcessMessage(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.PrichodCestuVozidlo:
                    ProcessPrichodCestuVozidlo(message);
                    break;

                case Mc.Nastupenie:
                    ProcessNastupenie(message);
                    break;

                case Mc.Presun:
                    ProcessPresun(message);
                    break;
                case Mc.PrichodVozidla:
                    ProcessPrichodVoz(message);
                    break;

                default:
                    ProcessDefault(message);
                    break;
            }
        }

        private void ProcessPrichodVoz(MessageForm message)
        {
            MyMessage ms = (MyMessage)message;
            ms.Addressee = MySim.FindAgent(SimId.AgentPresunu);
            ms.Code = Mc.Presun;
            Request(ms);

        }

        //meta! tag="end"
        public new AgentVozidiel MyAgent
        {
            get
            {
                return (AgentVozidiel)base.MyAgent;
            }
        }
    }
}