using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AC_Project.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void NeighboursTest()
        {
            int a, b, c, d;
            a = 0;
            b = 2;
            c = 4;
            d = 9;
            double error = 0.14;
            Neighbours target = new Neighbours(a, b, c, d);

            List<int> targetList = target.GetGroup();


            Assert.AreEqual(targetList[0], a);
            Assert.AreEqual(targetList[1], b);
            Assert.AreEqual(targetList[2], c);
            Assert.AreEqual(targetList[3], d);


            target.SetLocalBest(b, error);
            Assert.AreEqual(b, target.GetLocalBest());
            Assert.AreEqual(error, target.GetLocalMinError());
        }

        [TestMethod]
        public void HistoryTest()
        {
            int[] rel = { 0 };
            double[,] tmp1 = new double[4, 4] { { 0, 0, 0, 0 }, { 1, 0, 0, 0 }, { 0, 0, 1, 0 }, { 0, 1, 0, 1 } };
            double[,] tmp2 = new double[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 1, 0, 1, 0 }, { 0, 1, 0, 1 } };
            List<TransitionTable> SampleTable = new List<TransitionTable>();
            SampleTable.Add(new TransitionTable(4, tmp1));
            SampleTable.Add(new TransitionTable(4, tmp2));
            Random rand = new Random();
            int[] alphabet = { 0, 1 };
            Automata ideal = new Automata(4, alphabet, SampleTable, -1, 0.0, rel);

            Automata test1 = new Automata(4, alphabet, SampleTable, 1, 0.5, rel);
            Automata test2 = new Automata(4, alphabet, SampleTable, 2, 0.5, rel);
            Automata test3 = new Automata(4, alphabet, SampleTable, 3, 0.75, rel);


            History target = new History();

            target.AddGlobalBest(ideal);
            target.AddGlobalBest(test1);
            target.AddGlobalBest(test2);
            target.AddGlobalBest(test3);

            Automata IsGlobalBest = target.ReturnBestAutomata(test3, 4);

            Assert.AreEqual(ideal.GetId(), IsGlobalBest.GetId());

            target.Clear();

            Assert.AreEqual(ideal,target.ReturnBestAutomata(ideal,4));
            

        }

        [TestMethod]
        public void CalculateRelationsVectorTest()
        {
            int[] expected;
            int NumberOfWords = 100;


            double[,] tmp1 = new double[4, 4] { { 0, 0, 0, 0 }, { 1, 0, 0, 0 }, { 0, 0, 1, 0 }, { 0, 1, 0, 1 } };
            double[,] tmp2 = new double[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 1, 0, 1, 0 }, { 0, 1, 0, 1 } };
            List<TransitionTable> SampleTable = new List<TransitionTable>();
            SampleTable.Add(new TransitionTable(4, tmp1));
            SampleTable.Add(new TransitionTable(4, tmp2));
            Random rand = new Random();
            int[] alphabet = { 0, 1 };
            Automata target = new Automata(4, alphabet, SampleTable, -1);


        }
        
    }
}
