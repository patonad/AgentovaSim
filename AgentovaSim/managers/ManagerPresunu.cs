using System.Linq;
using OSPABA;
using simulation;
using agents;
using continualAssistants;
using PropertyChanged;

namespace managers
{
    //meta! id="4"
    [AddINotifyPropertyChangedInterface]

    public class ManagerPresunu : Manager
    {
        public ManagerPresunu(int id, Simulation mySim, Agent myAgent) :
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

        //meta! sender="AgentVozidiel", id="10", type="Request"
        public void ProcessPresun(MessageForm message)
        {
            //mam zacati presun jedneho auta
            MyMessage ms = (MyMessage)message.CreateCopy();
            ms.Addressee = MyAgent.FindAssistant(SimId.ProcesPresun);
            StartContinualAssistant(ms);


        }

        //meta! sender="ProcesPresun", id="20", type="Finish"
        public void ProcessFinish(MessageForm message)
        {
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
                case Mc.KoniecPresunu:
                    ProcesKoniecPresunu(message);
                    break;

                case Mc.Presun:
                    ProcessPresun(message);
                    break;

                default:
                    ProcessDefault(message);
                    break;
            }
        }

        private void ProcesKoniecPresunu(MessageForm message)
        {
            var ms = (MyMessage)message.CreateCopy();
            ms.Code = Mc.Presun;
            ms.Addressee = MySim.FindAgent(SimId.AgentVozidiel);
            Response(ms);
        }

        //meta! tag="end"
        public new AgentPresunu MyAgent
        {
            get
            {
                return (AgentPresunu)base.MyAgent;
            }
        }
    }
}