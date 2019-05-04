using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using agents;
using OSPABA;
using OSPRNG;
using simulation;

namespace AgentovaSim.continualAssistants
{
    class ProcesVystup : Process
    {
        public TriangularRNG TriangularRng { get; set; }
        public ProcesVystup(int id, Simulation mySim, CommonAgent myAgent) :
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
            var ms = (MyMessage)message;
            var vozidlo = ms.Vozidlo;
           // var zastavka = vozidlo.Linka.Presuny[ms.Vozidlo.AktualnyPresun].ZastavkaStart;
            int a = 0;
            if (ms.Vozidlo.PocetDvery <= vozidlo.Obsadene)
            {
                a = ms.Vozidlo.PocetDvery;
            }
            else
            {
                a = vozidlo.Obsadene;
            }
            for (int i = 0; i < a; i++)
            {
                if (!ms.Vozidlo.JePrazdny())
                {
                    ms = (MyMessage)message.CreateCopy();
                    ms.Code = Mc.VystupujeNiekto;
                    vozidlo.PocetObsadenychDvery++;
                    vozidlo.Vystup();
                    Hold(TriangularRng.Sample(), ms);
                }
            }

            if (vozidlo.PocetObsadenychDvery == 0)
            {
                ms.Addressee = MyAgent;
                ms.Code = Mc.KoniecVystupu;
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
                case Mc.VystupujeNiekto:
                    ProcessVystupujeNiekto(message);
                    break;
                default:
                    ProcessDefault(message);
                    break;
            }
        }

        private void ProcessVystupujeNiekto(MessageForm message)
        {
            MyMessage ms = (MyMessage)message;

            var vozidlo = ms.Vozidlo;
           // var zastavka = vozidlo.Linka.Presuny[ms.Vozidlo.AktualnyPresun].ZastavkaStart;
            if (!vozidlo.JePrazdny())
            {
                if (vozidlo.Obsadene != 0)
                {
                    ms.Code = Mc.VystupujeNiekto;
                    vozidlo.Vystup();
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
                ms.Code = Mc.KoniecVystupu;
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
