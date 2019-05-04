using AgentovaSim.continualAssistants;
using OSPABA;
using simulation;
using managers;
using continualAssistants;
using PropertyChanged;

namespace agents
{
    //meta! id="1"
    [AddINotifyPropertyChangedInterface]
    public class AgentModelu : Agent
	{
		public AgentModelu(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		    if (!((MySimulation)MySim).Fast)
		    {
		        MyMessage ms = new MyMessage(MySim);
		        ms.Addressee = FindAssistant(SimId.PlanovacStat);
                MyManager.StartContinualAssistant(ms);
		    }
        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerModelu(SimId.ManagerModelu, MySim, this);
            new PlanovacStat(SimId.PlanovacStat, MySim, this);
            AddOwnMessage(Mc.NovyCestujuci);
            AddOwnMessage(Mc.GeneujStat);
		}
		//meta! tag="end"
	}
}