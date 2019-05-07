using System;
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
        public UniformContinuousRNG UniformContinuousRNG { get; set; }
        public ProcesNastupenie(int id, Simulation mySim, CommonAgent myAgent) :
            base(id, mySim, myAgent)
        {
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication
            
            //TriangularRng = new TriangularRNG(0.6, 1.2, 4.2, new Random(1));
            //UniformContinuousRNG = new UniformContinuousRNG(6, 10, new Random(1));
            TriangularRng = new TriangularRNG(0.6, 1.2, 4.2, ((MySimulation)MySim).Random);
            UniformContinuousRNG = new UniformContinuousRNG(6,10, ((MySimulation)MySim).Random);
        }

        //meta! userInfo="Removed from model"

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
            }
        }

        private void Prerataj(Vozidlo vozidlo, Cestujuci ces)
        {
            vozidlo.Linka.Nastupeny++;
            //Console.WriteLine("nastupuje " + MySim.CurrentTime);
           // Console.WriteLine("caka " + (MySim.CurrentTime - ces.ZaciatokCakania));
            vozidlo.Linka.SucetCasCakana += MySim.CurrentTime - ces.ZaciatokCakania;
            var b = vozidlo.Linka.SucetCasCakana / vozidlo.Linka.Nastupeny;
            b = Double.IsNaN(b) ? 0 : b;
            vozidlo.Linka.PrCasCakana = b;

            ((MySimulation)MySim).Nastupeny++;
            ((MySimulation)MySim).SucetCasCakana += MySim.CurrentTime - ces.ZaciatokCakania;
            b = ((MySimulation)MySim).SucetCasCakana / ((MySimulation)MySim).Nastupeny;
            b = Double.IsNaN(b) ? 0 : b;
            ((MySimulation)MySim).PrCasCakana = b;
            if (vozidlo.Typ == "M")
            {
                ((MySimulation) MySim).Zarobok++;
            }

        }
        //meta! sender="AgentZasrtavok", id="38", type="Start"
        public void ProcessStart(MessageForm message)
        {
            var ms = (MyMessage)message;
            var vozidlo = ms.Vozidlo;
            var zastavka = vozidlo.Linka.Presuny[ms.Vozidlo.AktualnyPresun].ZastavkaStart;
            if (vozidlo.Typ == "A")
            {
                

                int a = 0;
                if (ms.Vozidlo.PocetDvery - vozidlo.PocetObsadenychDvery <= zastavka.PocetCestujucich)
                {
                    a = ms.Vozidlo.PocetDvery - vozidlo.PocetObsadenychDvery ;
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
                        var ces = zastavka.Dequeue();
                        vozidlo.Nastup(ces);
                        Prerataj(vozidlo,ces);
                        Hold(TriangularRng.Sample(),ms);
                       // Hold(3.1, ms);
                    }
                }

                if (vozidlo.PocetObsadenychDvery == 0)
                {
                    ms.Addressee = MyAgent;
                    ms.Code = Mc.KoniecNastupu;
                    Notice(ms);
                }
            }
            else
            {
                if (zastavka.PocetCestujucich != 0 && zastavka.Peek().DobaCakania(MySim.CurrentTime) >360 && !vozidlo.JePlny())
                {
                   ms = (MyMessage)message.CreateCopy();
                    ms.Code = Mc.NastupojeNiekto;
                    vozidlo.PocetObsadenychDvery++;
                    var ces = zastavka.Dequeue();
                    vozidlo.Nastup(ces);
                    Prerataj(vozidlo, ces);
                    Hold(UniformContinuousRNG.Sample(), ms);
                    //Hold(3.1, ms);
                }
                else
                {
                    ms.Addressee = MyAgent;
                    ms.Code = Mc.KoniecNastupu;
                    Notice(ms);
                }
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
           // Console.WriteLine(zastavka.Nazov + " Nastupi" + MySim.CurrentTime);
            if (vozidlo.Typ =="A")
            {
                if (!vozidlo.JePlny())
                {
                    if (zastavka.PocetCestujucich != 0)
                    {
                       
                        ms.Code = Mc.NastupojeNiekto;
                        var ces = zastavka.Dequeue();
                        vozidlo.Nastup(ces);
                        Prerataj(vozidlo, ces);
                        Hold(TriangularRng.Sample(), ms);
                        //Hold(3.1, ms);
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
            else
            {
                if (zastavka.PocetCestujucich != 0 && zastavka.Peek().DobaCakania(MySim.CurrentTime) > 360 && !vozidlo.JePlny())
                {
                    ms = (MyMessage)message.CreateCopy();
                    ms.Code = Mc.NastupojeNiekto;
                    vozidlo.PocetObsadenychDvery++;
                    var ces = zastavka.Dequeue();
                    vozidlo.Nastup(ces);
                    Prerataj(vozidlo, ces);
                    Hold(UniformContinuousRNG.Sample(), ms);
                    //Hold(3.1, ms);
                }
                else
                {
                    ms.Addressee = MyAgent;
                    ms.Code = Mc.KoniecNastupu;
                    Notice(ms);
                }
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