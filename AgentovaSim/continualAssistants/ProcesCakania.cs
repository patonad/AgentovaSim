using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using agents;
using OSPABA;
using simulation;

namespace AgentovaSim.continualAssistants
{
    class ProcesCakania :Process
    {
        public ProcesCakania(int id, Simulation mySim, CommonAgent myAgent) :
            base(id, mySim, myAgent)
        {
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication
          
        }

        //meta! userInfo="Removed from model"

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
            }
        }

        //meta! sender="AgentZasrtavok", id="38", type="Start"
        public void ProcessStart(MessageForm message)
        {
           // Console.WriteLine("Autobus zacina cakat " + MySim.CurrentTime);
            var ms = (MyMessage)message;
            ms.Code = Mc.Cakanie;
            Hold(90,message);
            

        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        override public void ProcessMessage(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.Start:
                    ProcessStart(message);
                    break;
                case Mc.Cakanie:
                    ProcessKoniecCakanie(message);
                    break;
                default:
                    ProcessDefault(message);
                    break;
            }
        }

        private void ProcessKoniecCakanie(MessageForm message)
        {
            MyMessage ms = (MyMessage)message;
            ms.Addressee = MyAgent;
            ms.Code = Mc.KOniecCakanie;
            Notice(ms);

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
