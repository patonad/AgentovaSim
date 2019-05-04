using System.Windows.Documents;
using OSPABA;
using agents;
using managers;
using PropertyChanged;

namespace simulation
{
    [AddINotifyPropertyChangedInterface]
	public class MySimulation : Simulation
	{
	    public bool Fast { get; set; }
	    public int Pocet { get; set; }
	    public double SymCas { get; set; }
	    public int _linkaATyp1;
	    public int _linkaATyp2;
	    public int _linkaBTyp1;
	    public int _linkaBTyp2;
	    public int _linkaCTyp1;
	    public int _linkaCTyp2;

        private int _pocet;
		public MySimulation(int LinkaATyp1, int LinkaATyp2, int LinkaBTyp1, int LinkaBTyp2, int LinkaCTyp1, int LinkaCTyp2)
		{
			
		    _linkaATyp1 = LinkaATyp1;
		    _linkaATyp2 = LinkaATyp2;
		    _linkaBTyp1 = LinkaBTyp1;
		    _linkaBTyp2 = LinkaBTyp2;
		    _linkaCTyp1 = LinkaCTyp1;
		    _linkaCTyp2 = LinkaCTyp2;
		    Init();

        }

	    protected override void PrepareSimulation()
		{
			base.PrepareSimulation();
			// Create global statistcis
		}

	    protected override void PrepareReplication()
		{
			base.PrepareReplication();
            // Reset entities, queues, local statistics, etc...
		    ((ManagerModelu) AgentModelu.MyManager).Cislo = 0;

		}

	    protected override void ReplicationFinished()
		{
			// Collect local statistics into global, update UI, etc...
			base.ReplicationFinished();
		    _pocet += ((ManagerModelu)AgentModelu.MyManager).Cislo;
		    Pocet = _pocet / (CurrentReplication+1);

		}

	    protected override void SimulationFinished()
		{
			// Dysplay simulation results
			base.SimulationFinished();
		    

        }

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			AgentModelu = new AgentModelu(SimId.AgentModelu, this, null);
			AgentOkolia = new AgentOkolia(SimId.AgentOkolia, this, AgentModelu);
			AgentVozidiel = new AgentVozidiel(SimId.AgentVozidiel, this, AgentModelu);
			AgentPresunu = new AgentPresunu(SimId.AgentPresunu, this, AgentVozidiel);
			AgentZasrtavok = new AgentZasrtavok(SimId.AgentZasrtavok, this, AgentVozidiel);
		}
		public AgentModelu AgentModelu
		{ get; set; }
		public AgentOkolia AgentOkolia
		{ get; set; }
		public AgentVozidiel AgentVozidiel
		{ get; set; }
		public AgentPresunu AgentPresunu
		{ get; set; }
		public AgentZasrtavok AgentZasrtavok
		{ get; set; }
		//meta! tag="end"
	}
}