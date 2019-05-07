using System.Collections.Generic;
using System.Linq;
using AgentovaSim.continualAssistants;
using AgentovaSim.PomocneTriedy;
using OSPABA;
using simulation;
using managers;
using continualAssistants;
using PropertyChanged;

namespace agents
{
    [AddINotifyPropertyChangedInterface]
    //meta! id="28"
    public class AgentZasrtavok : Agent
    {
        public List<Zastavka> ZastavkaList { get; set; } = new List<Zastavka>();
        public AgentZasrtavok(int id, Simulation mySim, Agent parent) :
            base(id, mySim, parent)
        {
            Init();
            GenerujZastavky();
        }

        private void GenerujZastavky()
        {
            string[] listNazov =
            {
                "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "BA", "BB", "BC", "BD", "BE",
                "BF", "BG", "BH", "BI", "BJ", "CA", "CB", "CC", "CD", "CE", "CF", "CG", "K1", "K2", "K3", "ST"
            };
            //string[] listNazov =
            //{
            //    "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "BA", "BB", "BC", "BD", "BE",
            //    "BF", "BG", "BH", "BI", "BJ", "CA", "CB", "CC", "CD", "CE", "CF", "CG", "ST"
            //};
            for (int i = 0; i < listNazov.Length; i++)
            {
                ZastavkaList.Add(new Zastavka(listNazov[i]));
            }

            var parent = (AgentVozidiel)Parent;
            string[] linkaA = { "AA", "AB", "AC", "AD", "K1", "AE", "AF", "AG", "K3", "AH", "AI", "AJ", "AK", "AL", "ST" };
            double[] presunA = { 3.2, 2.3, 2.1, 1.2, 5.4, 2.9, 3.4, 1.8, 4, 1.6, 4.6, 3.4, 1.2, 0.9, 25 };

            for (int i = 0; i < linkaA.Length; i++)
            {
                parent.LinkaA.Presuny.Add(new Presun(ZastavkaList.Where(x => x.Nazov == linkaA[i]).FirstOrDefault(), ZastavkaList.Where(x => x.Nazov == linkaA[(i + 1) % linkaA.Length]).FirstOrDefault(), presunA[i] * 60));
            }
            string[] linkaB = { "BA", "BB", "BC", "BD", "K2", "BE", "BF", "K3", "BG", "BH", "BI", "BJ", "ST" };
            double[] presunB = { 1.2, 2.3, 3.2, 4.3, 1.2, 2.7, 3, 6, 4.3, 0.5, 2.7, 1.3, 10 };

            for (int i = 0; i < linkaB.Length; i++)
            {
                parent.LinkaB.Presuny.Add(new Presun(ZastavkaList.Where(x => x.Nazov == linkaB[i]).FirstOrDefault(), ZastavkaList.Where(x => x.Nazov == linkaB[(i + 1) % linkaB.Length]).FirstOrDefault(), presunB[i] * 60));
            }
            string[] linkaC = { "CA", "CB", "K1", "K2", "CC", "CD", "CE", "CF", "CG", "ST" };
            double[] presunC = { 0.6, 2.3, 4.1, 6, 2.3, 7.1, 4.8, 3.7, 7.2, 30 };

            for (int i = 0; i < linkaC.Length; i++)
            {
                parent.LinkaC.Presuny.Add(new Presun(ZastavkaList.Where(x => x.Nazov == linkaC[i]).FirstOrDefault(), ZastavkaList.Where(x => x.Nazov == linkaC[(i + 1) % linkaC.Length]).FirstOrDefault(), presunC[i] * 60));
            }
            //var parent = (AgentVozidiel)Parent;
            //string[] linkaA = { "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "ST" };
            //double[] presunA = { 3.2, 2.3, 2.1, 1.2, 2.9, 3.4, 1.8, 1.6, 4.6, 3.4, 1.2, 0.9, 25 };

            //for (int i = 0; i < linkaA.Length; i++)
            //{
            //    parent.LinkaA.Presuny.Add(new Presun(ZastavkaList.Where(x => x.Nazov == linkaA[i]).FirstOrDefault(), ZastavkaList.Where(x => x.Nazov == linkaA[(i + 1) % linkaA.Length]).FirstOrDefault(), presunA[i] * 60));
            //}
            //string[] linkaB = { "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "ST" };
            //double[] presunB = { 1.2, 2.3, 3.2, 4.3, 2.7, 3, 4.3, 0.5, 2.7, 1.3, 10 };

            //for (int i = 0; i < linkaB.Length; i++)
            //{
            //    parent.LinkaB.Presuny.Add(new Presun(ZastavkaList.Where(x => x.Nazov == linkaB[i]).FirstOrDefault(), ZastavkaList.Where(x => x.Nazov == linkaB[(i + 1) % linkaB.Length]).FirstOrDefault(), presunB[i] * 60));
            //}
            //string[] linkaC = { "CA", "CB",  "CC", "CD", "CE", "CF", "CG", "ST" };
            //double[] presunC = { 0.6, 2.3, 2.3, 7.1, 4.8, 3.7, 7.2, 30 };

            //for (int i = 0; i < linkaC.Length; i++)
            //{
            //    parent.LinkaC.Presuny.Add(new Presun(ZastavkaList.Where(x => x.Nazov == linkaC[i]).FirstOrDefault(), ZastavkaList.Where(x => x.Nazov == linkaC[(i + 1) % linkaC.Length]).FirstOrDefault(), presunC[i] * 60));
            //}
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication
            //pre test zakomentovane
            //foreach (var zastavka in ZastavkaList)
            //{
            //    zastavka.PocetCestujucich = 0;
            //}




        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        private void Init()
        {
            new ManagerZasrtavok(SimId.ManagerZasrtavok, MySim, this);
            new ProcesNastupenie(SimId.ProcesNastupenie, MySim, this);
            new ProcesVystup(SimId.ProcesVystupenie, MySim, this);
            new ProcesCakania(SimId.ProcesCakania, MySim, this);
            AddOwnMessage(Mc.PrichodCestuZastavka);
            AddOwnMessage(Mc.Nastupenie);
            AddOwnMessage(Mc.NastupojeNiekto);
            AddOwnMessage(Mc.KoniecNastupu);
            AddOwnMessage(Mc.VystupujeNiekto);
            AddOwnMessage(Mc.KoniecVystupu);
            AddOwnMessage(Mc.Cakanie);
            AddOwnMessage(Mc.KOniecCakanie);
        }

        //meta! tag="end"
    }
}
