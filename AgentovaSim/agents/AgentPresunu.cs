using OSPABA;
using simulation;
using managers;
using continualAssistants;

namespace agents
{
	//meta! id="4"
	public class AgentPresunu : Agent
	{
		public AgentPresunu(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerPresunu(SimId.ManagerPresunu, MySim, this);
			new ProcesPresun(SimId.ProcesPresun, MySim, this);
			AddOwnMessage(Mc.Presun);
		}
		//meta! tag="end"
	}
}