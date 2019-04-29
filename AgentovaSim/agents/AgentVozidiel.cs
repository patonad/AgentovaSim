using OSPABA;
using simulation;
using managers;
using continualAssistants;
namespace agents
{
	//meta! id="3"
	public class AgentVozidiel : Agent
	{
		public AgentVozidiel(int id, Simulation mySim, Agent parent) :
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
			new ManagerVozidiel(SimId.ManagerVozidiel, MySim, this);
			new ProcesNastupenie(SimId.ProcesNastupenie, MySim, this);
			AddOwnMessage(Mc.Presun);
			AddOwnMessage(Mc.Nastupenie);
		}
		//meta! tag="end"
	}
}
