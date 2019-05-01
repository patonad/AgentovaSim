using OSPABA;
using simulation;
using managers;
using continualAssistants;
namespace agents
{
	//meta! id="28"
	public class AgentZasrtavok : Agent
	{
		public AgentZasrtavok(int id, Simulation mySim, Agent parent) :
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
			new ManagerZasrtavok(SimId.ManagerZasrtavok, MySim, this);
			new ProcesNastupenie(SimId.ProcesNastupenie, MySim, this);
			AddOwnMessage(Mc.PrichodCestuZastavka);
			AddOwnMessage(Mc.Nastupenie);
		}
		//meta! tag="end"
	}
}
