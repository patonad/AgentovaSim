using agents;
using OSPABA;
using OSPStat;
using simulation;
using managers;
using continualAssistants;
using OSPDataStruct;
namespace simulation
{
	public class MySimulation : Simulation
	{
		public MySimulation()
		{
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
		}
	   

	    protected override void ReplicationFinished()
		{
			// Collect local statistics into global, update UI, etc...
			base.ReplicationFinished();
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
		}
		public AgentModelu AgentModelu
		{ get; set; }
		public AgentOkolia AgentOkolia
		{ get; set; }
		public AgentVozidiel AgentVozidiel
		{ get; set; }
		public AgentPresunu AgentPresunu
		{ get; set; }
        //meta! tag="end"
	   
    }
}
