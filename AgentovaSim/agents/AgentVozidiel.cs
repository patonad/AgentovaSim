using System.Linq;
using System.Windows.Shapes;
using AgentovaSim.PomocneTriedy;
using OSPABA;
using simulation;
using managers;
using continualAssistants;
using PropertyChanged;

namespace agents
{
    [AddINotifyPropertyChangedInterface]
    //meta! id="3"
    public class AgentVozidiel : Agent
	{
	    public Linka LinkaA { get; set; } = new Linka();
	    public Linka LinkaB { get; set; } = new Linka();
        public Linka LinkaC { get; set; } = new Linka();
        public AgentVozidiel(int id, Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
            // Setup component for the next replication
		    MyMessage ms = new MyMessage(MySim);
		    ms.Addressee = MySim.FindAgent(SimId.AgentPresunu);
		    ms.Vozidlo = ((AgentPresunu)MySim.FindAgent(SimId.AgentPresunu)).ListVozideil.FirstOrDefault();
		    ms.Code = Mc.Presun;
		    MyManager.Request(ms);

        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerVozidiel(SimId.ManagerVozidiel, MySim, this);
			AddOwnMessage(Mc.Presun);
			AddOwnMessage(Mc.PrichodCestuVozidlo);
			AddOwnMessage(Mc.Nastupenie);
        }
		//meta! tag="end"
	}
}