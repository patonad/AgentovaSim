using OSPABA;
using simulation;
using agents;
namespace continualAssistants
{
	//meta! id="19"
	public class ProcesPresun : Process
	{
		public ProcesPresun(int id, Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentPresunu", id="20", type="Start"
		public void ProcessStart(MessageForm message)
		{
		    var ms = (MyMessage) message.CreateCopy();
		    ms.Code = Mc.KoniecPresunu;
            var vozidlo = ms.Vozidlo;
		    var linka = vozidlo.Linka;
		    var prestup = linka.Presuny[vozidlo.AktualnyPresun];
		    vozidlo.Cesta = prestup.ZastavkaStart.Nazov + " - " +
		                    prestup.ZastavkaKoniec.Nazov;
		    vozidlo.Strat = MySim.CurrentTime;
		    vozidlo.Koniec = MySim.CurrentTime + prestup.CasPresunu;
            Hold(prestup.CasPresunu,ms);
		    vozidlo.AktualnyPresun++;
		    vozidlo.AktualnyPresun = vozidlo.AktualnyPresun % linka.Presuny.Count;

		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
		    var ms = (MyMessage)message.CreateCopy();
           
		    ms.Code = Mc.KoniecPresunu;
		    ms.Addressee = MyAgent;
		    Notice(ms);
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
		public new AgentPresunu MyAgent
		{
			get
			{
				return (AgentPresunu)base.MyAgent;
			}
		}
	}
}