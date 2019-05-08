using System;
using System.Windows.Documents;
using OSPABA;
using agents;
using AgentovaSim.simulation;
using managers;
using Microsoft.Win32;
using OSPStat;
using PropertyChanged;

namespace simulation
{
    [AddINotifyPropertyChangedInterface]
	public class MySimulation : Simulation
	{
	    public int AktualRepl { get; set; }

	    public int Pocet { get; set; } = 0;
	    public int PocetCelkovo { get; set; } = 0;
	    public double PocetPoRepl { get; set; } = 0;

	    public double PrCasCakana { get; set; }
	    public double SucetCasCakana { get; set; } = 0;
	    public int Nastupeny { get; set; } = 0;
	    public double PrCasCakanaSucet { get; set; }
	    public double PrCasCakanaPoRepl { get; set; }

	    public double PrCasCakanaPoReplA { get; set; }
	    public double PrCasCakanaPoReplB { get; set; }
	    public double PrCasCakanaPoReplC { get; set; }
	    public double PrCasCakanaPoReplASuc { get; set; }
	    public double PrCasCakanaPoReplBSuc { get; set; }
	    public double PrCasCakanaPoReplCSuc { get; set; }

	    public int PoZaciatku { get; set; } = 0;
	    public double PoZaciatkuPoRepl { get; set; } = 0;
	    public double PoZaciatkuSucet { get; set; } = 0;

	    public double PoZaciatkuSucetA { get; set; }
	    public double PoZaciatkuSucetB { get; set; }
	    public double PoZaciatkuSucetC { get; set; }

	    public double PoZaciatkuAPoRepl { get; set; }
	    public double PoZaciatkuBPoRepl { get; set; }
	    public double PoZaciatkuCPoRepl { get; set; }

        public Random Random { get; set; } = new Random();

        public bool Fast { get; set; }
	    public int Spomalenie { get; set; } = 1000000;
	    public int Naklady { get; set; } = 0;

	    public int Zarobok { get; set; } = 0;
	    public int ZarobokCelko { get; set; } = 0;
	    public double ZarobokPoRepl { get; set; } = 0;


        public int PocetVRepl { get; set; } = 0;
	   

	    public double PoZaciatkuPomer { get; set; } = 0;
	    public TimeSpan CasZapasu { get; set; }
	    public TimeSpan AktualCas { get; set; }
        public double SymCas { get; set; }
	    public int _linkaATyp1;
	    public int _linkaATyp2;
	    public int _linkaAMicro;
        public int _linkaBTyp1;
	    public int _linkaBTyp2;
	    public int _linkaBMicro;
        public int _linkaCTyp1;
	    public int _linkaCTyp2;
	    public int _linkaCMicro;
        public bool _cakanie;
	    public Manazer Manazer { get; set; }    
	    public bool GenerujeSa { get; set; } = true;
	    public  int PocetAut { get; set; }
        private int _pocet = 1;


	    public Stat PocetPrichodovStat { get; set; } = new Stat();
	    public double PocetPrichodovStatL { get; set; }
	    public double PocetPrichodovStatP { get; set; }

	    public Stat PocetPrichodovPoZacStat { get; set; } = new Stat();
	    public double PocetPrichodovPoZacStatL { get; set; }
	    public double PocetPrichodovPoZacStatP { get; set; }


	    public Stat DobaCakaniaStat { get; set; } = new Stat();
	    public double DobaCakaniaStatL { get; set; }
	    public double DobaCakaniaStatP { get; set; }

	    public Stat ZarobokStat { get; set; } = new Stat();
	    public double ZarobokStatL { get; set; }
	    public double ZarobokStatP { get; set; }

        public MySimulation(int LinkaATyp1, int LinkaATyp2, int linkaAMicr , int LinkaBTyp1, int LinkaBTyp2, int linkaBMicr, int LinkaCTyp1, int LinkaCTyp2, int linkaCMicr)
		{
		    var typ1 = 0;
		    var typ2 = 0;
            _linkaATyp1 = LinkaATyp1;
		    _linkaATyp2 = LinkaATyp2;
		    _linkaBTyp1 = LinkaBTyp1;
		    _linkaBTyp2 = LinkaBTyp2;
		    _linkaCTyp1 = LinkaCTyp1;
		    _linkaCTyp2 = LinkaCTyp2;
		    _linkaAMicro = linkaAMicr;
		    _linkaBMicro = linkaBMicr;
		    _linkaCMicro = linkaCMicr;
		    PocetAut = LinkaATyp1 + LinkaATyp2 + linkaAMicr + LinkaBTyp1 + LinkaBTyp2 + linkaBMicr + LinkaCTyp1 +
		               LinkaCTyp2 + linkaCMicr;
		    typ1 += LinkaATyp1;
		    typ2 += LinkaATyp2;
		    typ1 += LinkaBTyp1;
		    typ2 += LinkaBTyp2;
		    typ1 += LinkaCTyp1;
		    typ2 += LinkaCTyp2;

		    Naklady = typ1 * 545000 + typ2 * 320000;

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
		    Nastupeny = 0;
		    PrCasCakana = 0;
		    SucetCasCakana = 0;
		    PoZaciatku = 0;
		    Zarobok = 0;


		}

	    protected override void ReplicationFinished()
		{

			// Collect local statistics into global, update UI, etc...
			base.ReplicationFinished();
		    AktualRepl = (CurrentReplication + 1);
            PocetCelkovo += ((ManagerModelu)AgentModelu.MyManager).Cislo;
		    PocetPoRepl = PocetCelkovo / (double)AktualRepl;
            
            PocetPrichodovStat.AddSample(((ManagerModelu)AgentModelu.MyManager).Cislo);
		   

            var cas = SucetCasCakana / Nastupeny;
		    PrCasCakanaSucet += cas; 
		    PrCasCakanaPoRepl = (PrCasCakanaSucet / AktualRepl)/60;
            //
		    

            PrCasCakanaPoReplASuc += AgentVozidiel.LinkaA.SucetCasCakana /
		                         AgentVozidiel.LinkaA.Nastupeny;
		    DobaCakaniaStat.AddSample(cas/60);

            PrCasCakanaPoReplBSuc += AgentVozidiel.LinkaB.SucetCasCakana /
		                         AgentVozidiel.LinkaB.Nastupeny;
		    PrCasCakanaPoReplCSuc += AgentVozidiel.LinkaC.SucetCasCakana /
		                         AgentVozidiel.LinkaC.Nastupeny;
		    PrCasCakanaPoReplA = (PrCasCakanaPoReplASuc/ (double)AktualRepl)/60;
		    PrCasCakanaPoReplB = (PrCasCakanaPoReplBSuc / (double)AktualRepl)/60;
		    PrCasCakanaPoReplC = (PrCasCakanaPoReplCSuc / (double)AktualRepl)/60;

            PoZaciatkuSucet += PoZaciatku / (double)Nastupeny *100;
		    PoZaciatkuPoRepl = PoZaciatkuSucet / AktualRepl;
		    PocetPrichodovPoZacStat.AddSample(PoZaciatku / (double)Nastupeny * 100);

		    ZarobokStat.AddSample(Zarobok);
            ZarobokCelko += Zarobok;
		    ZarobokPoRepl = (double)ZarobokCelko / AktualRepl;

		    PoZaciatkuSucetA += (AgentVozidiel.LinkaA.PoZaciatku / (double)AgentVozidiel.LinkaA.Nastupeny) * 100;
		    PoZaciatkuAPoRepl = PoZaciatkuSucetA / AktualRepl;

		    PoZaciatkuSucetB += (AgentVozidiel.LinkaB.PoZaciatku / (double)AgentVozidiel.LinkaB.Nastupeny) * 100;
		    PoZaciatkuBPoRepl = PoZaciatkuSucetB / AktualRepl;

		    PoZaciatkuSucetC += (AgentVozidiel.LinkaC.PoZaciatku / (double)AgentVozidiel.LinkaC.Nastupeny) * 100;
		    PoZaciatkuCPoRepl = PoZaciatkuSucetC / AktualRepl;


		    if (CurrentReplication > 10)
		    {
		        PocetPrichodovStatL = PocetPrichodovStat.ConfidenceInterval90[0];
		        PocetPrichodovStatP = PocetPrichodovStat.ConfidenceInterval90[1];

		        PocetPrichodovPoZacStatL = PocetPrichodovPoZacStat.ConfidenceInterval90[0];
		        PocetPrichodovPoZacStatP = PocetPrichodovPoZacStat.ConfidenceInterval90[1];

		        DobaCakaniaStatL = DobaCakaniaStat.ConfidenceInterval90[0];
		        DobaCakaniaStatP = DobaCakaniaStat.ConfidenceInterval90[1];

		        ZarobokStatL = ZarobokStat.ConfidenceInterval90[0];
		        ZarobokStatP = ZarobokStat.ConfidenceInterval90[1];


            }

        }

        protected override void SimulationFinished()
		{
			// Dysplay simulation results
			base.SimulationFinished();
		    _pocet += ((ManagerModelu)AgentModelu.MyManager).Cislo;
		    Pocet = _pocet / (CurrentReplication + 1);
		    Manazer.start = false;
		    Manazer.pouse = false;
		    Manazer.stop = true;



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