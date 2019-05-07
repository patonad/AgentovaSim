using System;
using System.Linq;
using OSPABA;
using simulation;
using agents;
using continualAssistants;
using PropertyChanged;

namespace managers
{
    //meta! id="28"
    [AddINotifyPropertyChangedInterface]
    public class ManagerZasrtavok : Manager
    {
        public ManagerZasrtavok(int id, Simulation mySim, Agent myAgent) :
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

        //meta! sender="AgentVozidiel", id="42", type="Notice"
        public void ProcessPrichodCestuZastavka(MessageForm message)
        {
            var ms = (MyMessage)message;
            var zastavka = MyAgent.ZastavkaList.FirstOrDefault(x => ms.Cestujuci.Zastavka != null && x.Nazov == ms.Cestujuci.Zastavka);
            zastavka.Enqueue(ms.Cestujuci);
            ms.Cestujuci.ZaciatokCakania = MySim.CurrentTime;
            //if (zastavka.Nazov == "AB")
            //{
            //    Console.WriteLine("AB "+ MySim.CurrentTime);
            //}
            if (zastavka.CakajuceVozidlo != null)
            {
                ms.Addressee = MyAgent.FindAssistant(SimId.ProcesNastupenie);
                ms.Vozidlo = zastavka.CakajuceVozidlo;
                StartContinualAssistant(ms);
            }
        }

        //meta! sender="ProcesNastupenie", id="38", type="Finish"
        public void ProcessFinish(MessageForm message)
        {
        }

        //meta! sender="AgentVozidiel", id="30", type="Request"
        public void ProcessNastupenie(MessageForm message)
        {
            MyMessage ms = (MyMessage)message.CreateCopy();
            var vozidlo = ms.Vozidlo;
            var zastavka = vozidlo.Linka.Presuny[vozidlo.AktualnyPresun].ZastavkaStart;
            

           //Console.WriteLine("Prisiel na zastavku " + zastavka.Nazov + " "+ MySim.CurrentTime);
            if (zastavka.Nazov != "ST")
            {
                if (zastavka.PocetCestujucich == 0)
                {
                    OdchodAutobusu(ms);
                    return;
                }
                ms.Addressee = MyAgent.FindAssistant(SimId.ProcesNastupenie);
                StartContinualAssistant(ms);
            }
            else
            {
                //tuto
                ms.Addressee = MyAgent.FindAssistant(SimId.ProcesVystupenie);
                StartContinualAssistant(ms);
            }


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
                case Mc.Finish:
                    ProcessFinish(message);
                    break;

                case Mc.PrichodCestuZastavka:
                    ProcessPrichodCestuZastavka(message);
                    break;

                case Mc.Nastupenie:
                    ProcessNastupenie(message);
                    break;

                case Mc.KoniecNastupu:
                    ProcessKoniecNastupu(message);
                    break;
                case Mc.KoniecVystupu:
                    ProcessKoniecVystupu(message);
                    break;
                case Mc.KOniecCakanie:
                    ProcessKoniecCakania(message);
                    break;
                default:
                    ProcessDefault(message);
                    break;
            }
        }

        private void ProcessKoniecCakania(MessageForm message)
        {
            var ms = (MyMessage)message;
            if (ms.Vozidlo.PocetObsadenychDvery == 0)
            {
                var vozidlo = ms.Vozidlo;
                var zastavka = vozidlo.Linka.Presuny[ms.Vozidlo.AktualnyPresun].ZastavkaStart;
                zastavka.CakajuceVozidlo = null;
                vozidlo.Caka = false;
                OdchodAutobusu(ms);
            }
            else
            {
                ms.Vozidlo.Odchod = true;
            }
        }

        private void ProcessKoniecVystupu(MessageForm message)
        {
            OdchodAutobusu(message);
        }

        private void ProcessKoniecNastupu(MessageForm message)
        {
            var ms = (MyMessage) message;
            var vozidlo = ms.Vozidlo;
            var zastavka = vozidlo.Linka.Presuny[ms.Vozidlo.AktualnyPresun].ZastavkaStart;
            if (vozidlo.Typ == "A")
            {
                if (vozidlo.JePlny())
                {
                    vozidlo.Caka = false;
                    if (zastavka.CakajuceVozidlo == vozidlo)
                    {
                        zastavka.CakajuceVozidlo = null;
                    }
                    OdchodAutobusu(message);
                    return;
                }
                if (((MySimulation)MySim)._cakanie && zastavka.CakajuceVozidlo == null)
                {
                    var ms2 =  new MyMessage(MySim);
                    ms2.Vozidlo = ms.Vozidlo;
                    ms2.Addressee = MyAgent.FindAssistant(SimId.ProcesCakania);
                    zastavka.CakajuceVozidlo = vozidlo;
                    vozidlo.Caka = true;
                    //dorobit nastupovanie pocas cakania
                    StartContinualAssistant(ms2);
                }
                else
                {
                    if (zastavka.CakajuceVozidlo != vozidlo)
                    {
                        vozidlo.Caka = false;
                        OdchodAutobusu(message);
                    }
                    else
                    {
                        if (vozidlo.Odchod && vozidlo.PocetObsadenychDvery ==0)
                        {
                            vozidlo.Caka = false;
                            zastavka.CakajuceVozidlo = null;
                            vozidlo.Odchod = false;
                            OdchodAutobusu(message);
                        }
                        else
                        {
                            ;
                        }
                    }
                }
            }
            else
            {
                OdchodAutobusu(ms);
            }
        }

        private void OdchodAutobusu(MessageForm message)
        {
            var ms = (MyMessage) message.CreateCopy();
            ms.Addressee = MySim.FindAgent(SimId.AgentVozidiel);
            ms.Code = Mc.Nastupenie;
            // ms.Vozidlo.Linka.Presuny[((MyMessage)message).Vozidlo.AktualnyPresun].ZastavkaStart.CakajuceVozidlo = null;
            Notice(ms);
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
