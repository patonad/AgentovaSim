using OSPABA;
using simulation;
using agents;
using AgentovaSim.PomocneTriedy;
using OSPRNG;

namespace continualAssistants
{
    //meta! id="16"
    public class ProcesNastupenie : Process
    {
        public TriangularRNG TriangularRng { get; set; }
        public ProcesNastupenie(int id, Simulation mySim, CommonAgent myAgent) :
            base(id, mySim, myAgent)
        {
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication
            TriangularRng = new TriangularRNG(0.6, 1.2, 4.2);
        }

        //meta! userInfo="Removed from model"

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
            }
        }

        //meta! sender="AgentZasrtavok", id="38", type="Start"
        public void ProcessStart(MessageForm message)
        {
            var ms = (MyMessage) message;
            var vozidlo = ms.Vozidlo;
            var zastavka = vozidlo.Linka.Presuny[ms.Vozidlo.AktualnyPresun].ZastavkaStart;
            int a = 0;
            if (ms.Vozidlo.PocetDvery <= zastavka.PocetCestujucich)
            {
                a = ms.Vozidlo.PocetDvery;
            }
            else
            {
                a = zastavka.PocetCestujucich;
            }
            for (int i = 0; i < a; i++)
            {
                if (!ms.Vozidlo.JePlny())
                {
                    ms = (MyMessage) message.CreateCopy();
                    ms.Code = Mc.NastupojeNiekto;
                    vozidlo.PocetObsadenychDvery++;
                    vozidlo.Nastup(zastavka.Dequeue());
                    Hold(TriangularRng.Sample(),ms);
                }
            }

            if (vozidlo.PocetObsadenychDvery == 0)
            {
                ms.Addressee = MyAgent;
                ms.Code = Mc.KoniecNastupu;
                Notice(ms);
            }

        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        override public void ProcessMessage(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.Start:
                    ProcessStart(message);
                    break;
                case Mc.NastupojeNiekto:
                    ProcessNastupijeNiekto(message);
                    break;
                default:
                    ProcessDefault(message);
                    break;
            }
        }

        private void ProcessNastupijeNiekto(MessageForm message)
        {
            MyMessage ms = (MyMessage) message;
          
            var vozidlo = ms.Vozidlo;
            var zastavka = vozidlo.Linka.Presuny[ms.Vozidlo.AktualnyPresun].ZastavkaStart;
            if (!vozidlo.JePlny())
            {
                if (zastavka.PocetCestujucich != 0)
                {
                    ms.Code = Mc.NastupojeNiekto;
                    vozidlo.Nastup(zastavka.Dequeue());
                    Hold(TriangularRng.Sample(), ms);
                    return;
                }
                vozidlo.PocetObsadenychDvery--;
            }
            else
            {
                vozidlo.PocetObsadenychDvery--;
            }

            if (vozidlo.PocetObsadenychDvery == 0)
            {
                ms.Addressee = MyAgent;
                ms.Code = Mc.KoniecNastupu;
                Notice(ms);
            }
        }

        //meta! tag="end"
        public new AgentZasrtavok MyAgent
        {
            get
            {
                return (AgentZasrtavok)base.MyAgent;
            }
        }
    }
}