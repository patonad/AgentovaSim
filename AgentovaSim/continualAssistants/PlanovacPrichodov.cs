using System;
using System.Collections.Generic;
using System.Linq;
using OSPABA;
using simulation;
using agents;
using AgentovaSim.PomocneTriedy;
using AgentovaSim.simulation;
using OSPRNG;

namespace continualAssistants
{
    //meta! id="13"
    public class PlanovacPrichodov : Scheduler
    {
        private List<GeneratorCestujucych> _generatory;
        public PlanovacPrichodov(int id, Simulation mySim, CommonAgent myAgent) :
            base(id, mySim, myAgent)
        {

        }
       
        override public void PrepareReplication()
        {
            _generatory = new List<GeneratorCestujucych>();
            base.PrepareReplication();
            string[] listNazov = { "AA", "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "BA", "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "CA", "CB", "CC", "CD", "CE", "CF", "CG" };
            int[] listPrichod = { 123, 92, 241, 123, 215, 245, 137, 132, 164, 124, 213, 185, 79, 69, 43, 127, 30, 69, 162, 90, 148, 171, 240, 310, 131, 190, 132, 128, 70 };
            double[] casy = { 0.1, 3.3, 5.6, 7.7, 14.3, 17.2, 20.6, 26.4, 28, 32.6, 36, 37.2, 5.4, 6.6, 8.9, 12.1, 17.6, 20.3, 29.3, 33.6, 34.1, 36.8, 0, 0.6, 13, 15.3, 22.4, 27.2, 30.9 };
            //for (int i = 0; i < listNazov.Length; i++)
            //{
            //    _generatory.Add(new GeneratorCestujucych((65.0 / listPrichod[i]) * 60, listNazov[i], casy[i], listPrichod[i],((MySimulation)MySim).Random) { MaxCasGen = casy[i] + 65 });
            //}
            ////_generatory.Add(new GeneratorCestujucych((65.0 / 260) * 60, "K1", 29.2, 260) { MaxCasGen = 29.2 + 65 });
            ////_generatory.Add(new GeneratorCestujucych((65.0 / 210) * 60, "K2", 21.7, 210) { MaxCasGen = 21.7 + 65 });
            ////_generatory.Add(new GeneratorCestujucych((65.0 / 220) * 60, "K3", 14.8, 220) { MaxCasGen = 14.8 + 65 });
            //_generatory.Add(new GeneratorCestujucych(((65.0 + 35.2 - 29.2) / 260) * 60, "K1", 29.2, 260, ((MySimulation)MySim).Random) { MaxCasGen = 35.2 + 65 });
            //_generatory.Add(new GeneratorCestujucych(((65.0 + 31.1 - 21.7) / 210) * 60, "K2", 21.7, 210, ((MySimulation)MySim).Random) { MaxCasGen = 31.1 + 65 });
            //_generatory.Add(new GeneratorCestujucych(((65.0 + 15.7 - 14.8) / 220) * 60, "K3", 14.8, 220, ((MySimulation)MySim).Random) { MaxCasGen = 15.7 + 65 });

            for (int i = 0; i < listNazov.Length; i++)
            {
                _generatory.Add(new GeneratorCestujucych((65.0 / listPrichod[i]) * 60, listNazov[i], casy[i], listPrichod[i], ((MySimulation)MySim).Random) { MaxCasGen = casy[i] + 65 });
            }
            //_generatory.Add(new GeneratorCestujucych(((65.0 + 35.2 - 29.2) / 260) * 60, "K1", 29.2, 260, ((MySimulation)MySim).Random) { MaxCasGen = 35.2 + 65 });
            //_generatory.Add(new GeneratorCestujucych(((65.0 + 31.1 - 21.7) / 210) * 60, "K2", 21.7, 210, ((MySimulation)MySim).Random) { MaxCasGen = 31.1 + 65 });
            //_generatory.Add(new GeneratorCestujucych(((65.0 + 15.7 - 14.8) / 220) * 60, "K3", 14.8, 220, ((MySimulation)MySim).Random) { MaxCasGen = 15.7 + 65 });
            _generatory.Add(new GeneratorCestujucych(((65.0 + 35.2 - 29.2) / 260) * 60, "K1", 2.9, 260, ((MySimulation)MySim).Random) { MaxCasGen = 73.9 });
            _generatory.Add(new GeneratorCestujucych(((65.0 + 31.1 - 21.7) / 210) * 60, "K2", 7, 210, ((MySimulation)MySim).Random) { MaxCasGen = 81.4 });
            _generatory.Add(new GeneratorCestujucych(((65.0 + 15.7 - 14.8) / 220) * 60, "K3", 22.4, 220, ((MySimulation)MySim).Random) { MaxCasGen = 88.3 });
        }

        //meta! sender="AgentOkolia", id="14", type="Start"
        public void ProcessStart(MessageForm message)
        {
            foreach (var gen in _generatory)
            {
                var ms = (MyMessage)message.CreateCopy();
                ms.Generator = gen;
                ms.Code = Mc.GenerujCestujuceho;
                 Hold(gen.Oneskorenie * 60 + gen.ExponentialRng.Sample(), ms);
                //Hold(gen.Oneskorenie * 60 + 300, ms);
            }




            //var gen = _generatory.Where(x => x.Nazov == "AB").FirstOrDefault();
            // var ms = (MyMessage)message.CreateCopy();
            // ms.Generator = gen;
            // ms.Code = Mc.GenerujCestujuceho;
            // Hold(0 * 60 + gen.ExponentialRng.Sample(), ms);
            //  gen = _generatory.Where(x => x.Nazov == "AC").FirstOrDefault();
            //  ms = (MyMessage)message.CreateCopy();
            // ms.Generator = gen;
            // ms.Code = Mc.GenerujCestujuceho;
            // Hold(0 * 60 + gen.ExponentialRng.Sample(), ms);
        }

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {

            switch (message.Code)
            {

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
                case Mc.GenerujCestujuceho:
                    ProcesGen(message);
                    break;
                case Mc.BreakCA:
                    ProcesBreak(message);
                    break;
                default:
                    ProcessDefault(message);
                    break;
            }
        }

        private void ProcesBreak(MessageForm message)
        {
            ;
        }

        private void ProcesGen(MessageForm message)
        {
            var ms = (MyMessage)message.CreateCopy();
            ms.Code = Mc.GenerujCestujuceho;
            ms.Addressee = MyAgent;
            ms.Generator.Kolko--;
            ms.Cestujuci = new Cestujuci() { Zastavka = ms.Generator.Nazov };
            Notice(ms);
            var cas = ms.Generator.ExponentialRng.Sample();
            //cas = 300;

            // if ((ms.Generator.MaxCasGen) *60 >= MySim.CurrentTime + cas)

            if ((ms.Generator.MaxCasGen) * 60.0 >= MySim.CurrentTime + cas && ms.Generator.Kolko > 0)
            {
                ms = (MyMessage)message.CreateCopy();
                ms.Code = Mc.GenerujCestujuceho;
                Hold(cas, ms);
            }
            else
            {
                ms.Generator.GenerujeSa = false;
               ((MySimulation)MySim).GenerujeSa = _generatory.Any(x => x.GenerujeSa);
            }
        }

        //meta! tag="end"
        public new AgentOkolia MyAgent
        {
            get
            {
                return (AgentOkolia)base.MyAgent;
            }
        }
    }
}