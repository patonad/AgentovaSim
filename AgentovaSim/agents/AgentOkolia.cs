using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AgentovaSim.PomocneTriedy;
using OSPABA;
using simulation;
using managers;
using continualAssistants;
namespace agents
{
    //meta! id="2"
    public class AgentOkolia : Agent
    {
        public List<Zastavka> ZastavkyLinkaA { get; set; } = new List<Zastavka>();
        public List<Zastavka> ZastavkyLinkaB { get; set; } = new List<Zastavka>();
        public List<Zastavka> ZastavkyLinkaC { get; set; } = new List<Zastavka>();
        public Linka LinkaA  { get; set; }
        public Linka LinkaB { get; set; }
        public Linka LinkaC { get; set; }
        public int PocetVygenerovanychZakaznikov { get; set; } = 0;

        public AgentOkolia(int id, Simulation mySim, Agent parent) :
            base(id, mySim, parent)
        {
            Init();
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication

            VytvorZastavky();
           //ZacniPlanovatCestujucich();

        }

        private void VytvorZastavky()
        {
            string[] listNazovA = { "AA", "AB", "AC", "AD", "K1", "AE", "AF", "AG", "K3", "AH", "AI", "AJ", "AK", "AL", "st" };
            string[] listNazovB = { "BA", "BB", "BC", "BD", "K2", "BE", "BF", "K3", "BG", "BH", "BI", "BJ", "st" };
            string[] listNazovC = { "CA", "CB", "K1", "K2", "CC", "CD", "CE", "CF", "CG", "st" };
            double[] listJazdyA = { 3.2, 2.3, 2.1, 1.2, 5.4, 2.9, 3.4, 1.8, 4.0, 1.6, 4.6, 3.4, 1.2, 0.9 , 25};
            double[] listJazdyB = { 1.2, 2.3, 3.2, 4.3, 1.2, 2.7, 3.0, 6.0, 4.3, 0.5, 2.7, 1.3, 10 };
            double[] listJazdyC = { 0.6, 2.3, 4.1, 6.0, 2.3, 7.1, 4.8, 3.7, 7.2 , 30};
            int[] listPrichodA = { 123, 92, 241, 123, 260, 215, 248, 137, 220, 132, 164, 124, 213, 185,0 };
            int[] listPrichodB = { 79, 69, 43, 127, 210, 30, 69, 220, 162, 90, 148, 171,0 };
            int[] listProchodC = { 240, 310, 260, 210, 131, 190, 132, 128, 70 ,0};

            MyMessage sprava = new MyMessage(MySim);
            sprava.Addressee = FindAssistant(SimId.PlanovacPrichodov);
            Zastavka K1 = new Zastavka("K1", 260);
            sprava.Zastavka = K1;
            MyManager.StartContinualAssistant(sprava);

            sprava = new MyMessage(MySim);
            sprava.Addressee = FindAssistant(SimId.PlanovacPrichodov);
            Zastavka K2 = new Zastavka("K2", 210);
            sprava.Zastavka = K2;
            MyManager.StartContinualAssistant(sprava);
            sprava = new MyMessage(MySim);
            sprava.Addressee = FindAssistant(SimId.PlanovacPrichodov);
            Zastavka K3 = new Zastavka("K3", 220);
            sprava.Zastavka = K3;
            MyManager.StartContinualAssistant(sprava);

            Zastavka st = new Zastavka("st", 0);
            LinkaA = new Linka();
            for (int i = 0; i < listJazdyA.Length; i++)
            {
                Zastavka zastavka;
                if (listNazovA[i] == "K1")
                {
                    zastavka = K1;
                }
                else if (listNazovA[i] == "K2")
                {
                    zastavka = K2;
                }
                else if(listNazovA[i] == "K3")
                {
                    zastavka = K3;
                } else if (listNazovA[i] == "st")
                {
                    zastavka = st;
                }

                else
                {
                    zastavka = new Zastavka(listNazovA[i], listPrichodA[i]);
                    sprava = new MyMessage(MySim);
                    sprava.Addressee = FindAssistant(SimId.PlanovacPrichodov);
                    sprava.Zastavka = zastavka;
                    MyManager.StartContinualAssistant(sprava);
                }
                LinkaA.Presuny.Add(new Presun(zastavka, listJazdyA[i]));
            }
            LinkaB = new Linka();
            for (int i = 0; i < listJazdyB.Length; i++)
            {
                Zastavka zastavka;
                if (listNazovB[i] == "K1")
                {
                    zastavka = K1;
                }
                else if (listNazovB[i] == "K2")
                {
                    zastavka = K2;
                }
                else if (listNazovB[i] == "K3")
                {
                    zastavka = K3;
                }
                else if (listNazovA[i] == "st")
                {
                    zastavka = st;
                }

                else
                {
                    zastavka = new Zastavka(listNazovB[i], listPrichodB[i]);
                    sprava = new MyMessage(MySim);
                    sprava.Addressee = FindAssistant(SimId.PlanovacPrichodov);
                    sprava.Zastavka = zastavka;
                    MyManager.StartContinualAssistant(sprava);
                }
                LinkaB.Presuny.Add(new Presun(zastavka, listJazdyB[i]));
            }
            LinkaC = new Linka();
            for (int i = 0; i < listJazdyC.Length; i++)
            {
                Zastavka zastavka;
                if (listNazovC[i] == "K1")
                {
                    zastavka = K1;
                }
                else if (listNazovC[i] == "K2")
                {
                    zastavka = K2;
                }
                else if (listNazovC[i] == "K3")
                {
                    zastavka = K3;
                }
                else if (listNazovC[i] == "st")
                {
                    zastavka = st;
                }

                else
                {
                    zastavka = new Zastavka(listNazovC[i], listProchodC[i]);
                    sprava = new MyMessage(MySim);
                    sprava.Addressee = FindAssistant(SimId.PlanovacPrichodov);
                    sprava.Zastavka = zastavka;
                    MyManager.StartContinualAssistant(sprava);
                }
                LinkaC.Presuny.Add(new Presun(zastavka, listJazdyC[i]));
            }
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        private void Init()
        {
            new ManagerOkolia(SimId.ManagerOkolia, MySim, this);
            new PlanovacPrichodov(SimId.PlanovacPrichodov, MySim, this);
            AddOwnMessage(SimId.PlanovacPrichodov);
        }
        //private void ZacniPlanovatCestujucich()
        //{
        //    MyMessage sprava = new MyMessage(MySim);
        //    sprava.Addressee = FindAssistant(SimId.PlanovacPrichodov);
        //    MyManager.StartContinualAssistant(sprava);
        //}
        //meta! tag="end"
    }
}
