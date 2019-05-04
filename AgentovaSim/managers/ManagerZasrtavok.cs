using System.Linq;
using OSPABA;
using simulation;
using agents;
using continualAssistants;
using PropertyChanged;

namespace managers
{
    //meta! id="28"
    [AddINotifyPropertyChangedInterface]
    public class ManagerZasrtavok : Manager
    {
        public ManagerZasrtavok(int id, Simulation mySim, Agent myAgent) :
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

        //meta! sender="AgentVozidiel", id="42", type="Notice"
        public void ProcessPrichodCestuZastavka(MessageForm message)
        {
            var ms = (MyMessage)message;
            MyAgent.ZastavkaList.FirstOrDefault(x => ms.Cestujuci.Zastavka != null && x.Nazov == ms.Cestujuci.Zastavka).Enqueue(ms.Cestujuci);
        }

        //meta! sender="ProcesNastupenie", id="38", type="Finish"
        public void ProcessFinish(MessageForm message)
        {
        }

        //meta! sender="AgentVozidiel", id="30", type="Request"
        public void ProcessNastupenie(MessageForm message)
        {
            MyMessage ms = (MyMessage)message.CreateCopy();
            var vozidlo = ms.Vozidlo;
            var zastavka = vozidlo.Linka.Presuny[vozidlo.AktualnyPresun].ZastavkaStart;
            if (zastavka.Nazov !="ST")
            {
                ms.Addressee = MyAgent.FindAssistant(SimId.ProcesNastupenie);
                StartContinualAssistant(ms);
            }
            else
            {
                //tuto
                ms.Addressee = MyAgent.FindAssistant(SimId.ProcesVystupenie);
                StartContinualAssistant(ms);
            }


        }

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
            }
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        public void Init()
        {
        }

        override public void ProcessMessage(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.Finish:
                    ProcessFinish(message);
                    break;

                case Mc.PrichodCestuZastavka:
                    ProcessPrichodCestuZastavka(message);
                    break;

                case Mc.Nastupenie:
                    ProcessNastupenie(message);
                    break;

                case Mc.KoniecNastupu:
                    ProcessKoniecNastupu(message);
                    break;
                case Mc.KoniecVystupu:
                    ProcessKoniecNastupu(message);
                    break;
                default:
                    ProcessDefault(message);
                    break;
            }
        }

        private void ProcessKoniecNastupu(MessageForm message)
        {
            message.Addressee = MySim.FindAgent(SimId.AgentVozidiel);
            message.Code = Mc.Nastupenie;
            Response(message);
        }

        //meta! tag="end"
        public new AgentZasrtavok MyAgent
        {
            get
            {
                return (AgentZasrtavok)base.MyAgent;
            }
        }
    }
}
