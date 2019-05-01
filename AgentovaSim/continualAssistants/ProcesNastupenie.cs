using OSPABA;
using simulation;
using agents;
namespace continualAssistants
{
	//meta! id="16"
	public class ProcesNastupenie : Process
	{
		public ProcesNastupenie(int id, Simulation mySim, CommonAgent myAgent) :
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
		public new AgentVozidiel MyAgent
		{
			get
			{
				return (AgentVozidiel)base.MyAgent;
			}
		}
	}
}