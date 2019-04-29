using OSPABA;
using simulation;
using agents;
using AgentovaSim.PomocneTriedy;

namespace continualAssistants
{
    //meta! id="13"
    public class PlanovacPrichodov : Scheduler
    {

        public PlanovacPrichodov(int id, Simulation mySim, CommonAgent myAgent) :
            base(id, mySim, myAgent)
        {
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication
        }

        //meta! sender="AgentOkolia", id="14", type="Start"
        public void ProcessStart(MessageForm message)
        {
            MyMessage ms = (MyMessage)message.CreateCopy();
            ms.Code = SimId.PlanovacPrichodov;
            Hold(ms.Zastavka.ExponentialRng.Sample(), ms);
        }

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
                case SimId.PlanovacPrichodov:
                    MyMessage ms = (MyMessage) message;
                    Hold(ms.Zastavka.ExponentialRng.Sample(),message.CreateCopy());
                   
                    ms = (MyMessage) message.CreateCopy();
                    ms.Cestujuci = new Cestujuci();
                    ms.Addressee = MyAgent;
                    Notice(ms);
                    break;
                default:
                    ;
                    break;
            }
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        override public void ProcessMessage(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.Start:
                    ProcessStart(message);
                    break;
                default:
                    ProcessDefault(message);
                    break;
            }
        }
        //meta! tag="end"
        public new AgentOkolia MyAgent
        {
            get
            {
                return (AgentOkolia)base.MyAgent;
            }
        }
    }
}
