using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using agents;
using continualAssistants;
using OSPABA;
using simulation;

namespace AgentovaSim.continualAssistants
{
    class PlanovacStat : Scheduler
    {
        public PlanovacStat(int id, Simulation mySim, CommonAgent myAgent) : base(id, mySim, myAgent)
        {
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication
        }

        //meta! sender="AgentPresunu", id="20", type="Start"
        public void ProcessStart(MessageForm message)
        {
            var ms = (MyMessage)message.CreateCopy();
            ms.Addressee = this;
            ms.Code = Mc.GeneujStat;
            ((MySimulation)MySim).SymCas = MySim.CurrentTime;
            Hold(1, ms);
        }

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            var ms = (MyMessage)message.CreateCopy();
            ms.Code = Mc.GeneujStat;
            ms.Addressee = this;
            ((MySimulation)MySim).SymCas = MySim.CurrentTime;
            Thread.SpinWait(((MySimulation)MySim).Spomalenie);
            PreratajStat();
            if (((MySimulation)MySim).GenerujeSa || ((MySimulation)MySim).PocetAut !=0)
            {
                Hold(1, ms);
            }
        }

        private void PreratajStat()
        {
            var ag = (AgentPresunu) MySim.FindAgent(SimId.AgentPresunu);
            foreach (var vozidlo in ag.ListVozideil)
            {
                vozidlo.Prerataj(MySim.CurrentTime);
            }

            var a = ((MySimulation) MySim).CasZapasu.TotalSeconds - 6786 + MySim.CurrentTime;
            ((MySimulation)MySim).AktualCas = TimeSpan.FromSeconds(a);
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
        public new AgentModelu MyAgent
        {
            get
            {
                return (AgentModelu)base.MyAgent;
            }
        }
    }
}
