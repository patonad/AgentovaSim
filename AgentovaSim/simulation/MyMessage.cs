using AgentovaSim.PomocneTriedy;
using OSPABA;
namespace simulation
{
	public class MyMessage : MessageForm
	{
	    public Zastavka Zastavka { get; set; } = null;
	    public Cestujuci Cestujuci { get; set; } = null;
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
		    Zastavka = original.Zastavka;
		    Cestujuci = original.Cestujuci;
		}
	}
}
