using OSPABA;
using simulation;
using agents;
using continualAssistants;
namespace managers
{
	//meta! id="28"
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
		}

		//meta! sender="ProcesNastupenie", id="38", type="Finish"
		public void ProcessFinish(MessageForm message)
		{
		}

		//meta! sender="AgentVozidiel", id="30", type="Request"
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

			default:
				ProcessDefault(message);
			break;
			}
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
