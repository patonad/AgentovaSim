using AgentovaSim.PomocneTriedy;
using AgentovaSim.simulation;
using OSPABA;
namespace simulation
{
	public class MyMessage : MessageForm
	{
	    public GeneratorCestujucych Generator { get; set; }
	    public Cestujuci Cestujuci { get; set;}
	    public Vozidlo Vozidlo { get; set; }

	    public double Oneskorenie { get; set; }
	  //  public Linka Linka { get; set; }    

	    public MyMessage(Simulation sim) :
			base(sim)
		{
		}

		public MyMessage(MyMessage original) :
			base(original)
		{
			// copy() is called in superclass
		}

		override public MessageForm CreateCopy()
		{
			return new MyMessage(this);
		}

		override protected void Copy(MessageForm message)
		{
			base.Copy(message);
			MyMessage original = (MyMessage)message;
		    Generator = original.Generator;
		    Cestujuci = original.Cestujuci;
		    Vozidlo = original.Vozidlo;
		    Oneskorenie = original.Oneskorenie;
		    // Linka = original.Linka;
		}
	}
}