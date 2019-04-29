using OSPABA;
using simulation;
using agents;
using continualAssistants;
namespace managers
{
	//meta! id="1"
	public class ManagerModelu : Manager
	{
		public ManagerModelu(int id, Simulation mySim, Agent myAgent) :
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

		//meta! sender="AgentOkolia", id="8", type="Notice"
		public void ProcessNotice(MessageForm message)
		{
		}

		//meta! sender="AgentVozidiel", id="9", type="Response"
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
			case Mc.Notice:
				ProcessNotice(message);
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
		public new AgentModelu MyAgent
		{
			get
			{
				return (AgentModelu)base.MyAgent;
			}
		}
	}
}
