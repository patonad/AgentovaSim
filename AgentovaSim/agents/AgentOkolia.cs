using OSPABA;
using simulation;
using managers;
using continualAssistants;
using PropertyChanged;

namespace agents
{
    [AddINotifyPropertyChangedInterface]
    //meta! id="2"
    public class AgentOkolia : Agent
	{
		public AgentOkolia(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
		    ZacniPlanovatCestujucich();
		}

	    private void ZacniPlanovatCestujucich()
	    {
	        MyMessage sprava = new MyMessage(MySim);
	        sprava.Addressee = FindAssistant(SimId.PlanovacPrichodov);
	        MyManager.StartContinualAssistant(sprava);
        }

	    //meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerOkolia(SimId.ManagerOkolia, MySim, this);
			new PlanovacPrichodov(SimId.PlanovacPrichodov, MySim, this);
            AddOwnMessage(Mc.GenerujCestujuceho);
		}
		//meta! tag="end"
	}
}