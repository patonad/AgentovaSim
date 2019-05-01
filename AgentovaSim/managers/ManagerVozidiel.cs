using OSPABA;
using simulation;
using agents;
using continualAssistants;

namespace managers
{
	//meta! id="3"
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
		}

		//meta! userInfo="Removed from model"
		public void ProcessFinish(MessageForm message)
		{
		}

		//meta! userInfo="Removed from model"
		public void ProcessNastupenie(MessageForm message)
		{
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

			default:
				ProcessDefault(message);
			break;
			}
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