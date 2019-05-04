using OSPABA;
using simulation;
using agents;
using continualAssistants;
using PropertyChanged;

namespace managers
{
    //meta! id="1"
    [AddINotifyPropertyChangedInterface]
    public class ManagerModelu : Manager
	{
	    public int Cislo { get; set; } = 0;
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
		public void ProcessNovyCestujuci(MessageForm message)
		{
		    Cislo++;
		    var ms = (MyMessage) message.CreateCopy();
		    ms.Addressee = MySim.FindAgent(SimId.AgentVozidiel);
		    ms.Code = Mc.PrichodCestuVozidlo;
            Notice(ms);

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

		//meta! userInfo="Generated code: do not modify", tag="begin"
		public void Init()
		{
		}

		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.NovyCestujuci:
				ProcessNovyCestujuci(message);
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