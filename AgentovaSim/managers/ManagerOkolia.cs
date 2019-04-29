using System.Collections.Generic;
using OSPABA;
using simulation;
using agents;
using AgentovaSim.PomocneTriedy;
using continualAssistants;
namespace managers
{
	//meta! id="2"
	public class ManagerOkolia : Manager
	{
	    
        public ManagerOkolia(int id, Simulation mySim, Agent myAgent) :
			base(id, mySim, myAgent)
		{
			Init();
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
            // Setup component for the next replication
		    
		  

        }

		//meta! sender="PlanovacPrichodov", id="14", type="Finish"
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
		    case Mc.PrisielCestujuci:
                    ProcessNovyZak(message);
			        break;

                default:
				ProcessDefault(message);
			break;
			}
		}

	    private void ProcessNovyZak(MessageForm message)
	    {
	        message.Code = Mc.PrisielCestujuci;
	        message.Addressee = MySim.FindAgent(SimId.AgentModelu);
	        Notice(message);
	        MyAgent.PocetVygenerovanychZakaznikov += 1;
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
