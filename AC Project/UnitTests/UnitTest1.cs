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
    public class UnitTest1
    {
            double aError = 0.005;
            int NumOfWords = 100;
            int LengthOfWordsFrom = 9;
            int LengthOfWordsTo = 100;
            int MaxIterations = 500;
            int n = 100; 
            int[] alphabet = {0,1};
            int nrLetters = 2;
            Random rand = new Random();
            int constant = 3;

        [TestMethod]
        public void GenerateWordsTest()
        {
           List<Word[]> samplelist = WordGenerator.GenerateWords(alphabet, nrLetters, rand, NumOfWords, LengthOfWordsFrom, LengthOfWordsTo,constant);
           Word[] testWords = samplelist[0];
           Word[] subset1 = samplelist[1];
           Word[] subset2 = samplelist[2];

            int allCount = testWords.Count();
            int cCount = subset1.Count();
            int gcCount = subset2.Count();
            //checking if number of words is good.
            Assert.AreEqual(100, allCount);
            Assert.AreEqual(14, cCount);
            Assert.AreEqual(86, gcCount);
            int index = 2;
            //checking if word index is correct;
            int actualID = testWords[index].getId();
            Assert.AreEqual(0, actualID);
        }
        [TestMethod]
        public void GenerateTestWordsTest()
    {
             List<Word[]> samplelist = WordGenerator.GenerateWords(alphabet, nrLetters, rand, NumOfWords, LengthOfWordsFrom, LengthOfWordsTo,constant);
           Word[] testWords = samplelist[0];
            int actualCount = testWords.Count();
            //checking if number of words is good.
            Assert.AreEqual(100, actualCount);
            int index = 2;
            //checking if word index is correct;
            int actualID = testWords[index].getId();
            Assert.AreEqual(0, actualID);
    }

        [TestMethod]
        public void AutomataTransitionTest()
        {

            double[,] tmp1 = new double[4, 4] { { 0, 0, 0, 0 }, { 1, 0, 0, 0 }, { 0, 0, 1, 0 }, { 0, 1, 0, 1 } };
            double[,] tmp2 = new double[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 1, 0, 1, 0 }, { 0, 1, 0, 1 } };
            List<TransitionTable> SampleTable = new List<TransitionTable>();
            SampleTable.Add(new TransitionTable(4, tmp1));
            SampleTable.Add(new TransitionTable(4, tmp2));
            Random rand = new Random();
            int[] alphabet = { 0, 1 };
            Automata target = new Automata(4, alphabet, SampleTable, -1);
            int[] ActualAlphabet = target.getAlphabet();
            Assert.AreEqual(4, target.GetTransitionTable(0).getSize());
            Assert.AreEqual(4, target.GetTransitionTable(1).getSize());
            Assert.AreEqual(alphabet, ActualAlphabet);
            List<TransitionTable> ActualList = target.GetTransitionTables();
            Assert.AreEqual(ActualList, SampleTable);
            TransitionTable ActualTransitionTable = target.GetTransitionTable(0);
            Assert.AreEqual(tmp1, ActualTransitionTable.GetTransitionMatrix());
            ActualTransitionTable = target.GetTransitionTable(1);
            Assert.AreEqual(tmp2, ActualTransitionTable.GetTransitionMatrix());
            Assert.AreEqual(4, target.getStates());
            Assert.AreEqual(2, target.getAlphabetSize());
            target.getPosition();
            int[] expectedposition =  {0,1,0,0,0,0,0,1,0,0,1,0,0,0,0,1,0,0,1,0,0,0,0,1,0,0,1,0,0,0,0,1	};
            for (int i = 0; i < 32; i++ )
                Assert.AreEqual(expectedposition[i], target.getPosition()[i]);


        }
           [TestMethod]
        public void AutomataCompute()
        {
            double[,] tmp1 = new double[4, 4] { { 0, 0, 0, 0 }, { 1, 0, 0, 0 }, { 0, 0, 1, 0 }, { 0, 1, 0, 1 } };
            double[,] tmp2 = new double[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 1, 0, 1, 0 }, { 0, 1, 0, 1 } };
            List<TransitionTable> SampleTable = new List<TransitionTable>();
            SampleTable.Add(new TransitionTable(4, tmp1));
            SampleTable.Add(new TransitionTable(4, tmp2));
            Random rand = new Random();
            int[] alphabet = { 0, 1 };
            Automata TestAutomata = new Automata(4, alphabet, SampleTable, -1);
            List<Word[]> samplelist = WordGenerator.GenerateWords(alphabet, nrLetters, rand, 30, LengthOfWordsFrom, LengthOfWordsTo, constant);
            Word[] testWords = samplelist[0];
            TestAutomata.ComputeAutomata(testWords);
            int[] relations = TestAutomata.GetRelations();
            Assert.AreEqual(relations.Count(), 435);
            int[] PositionBefore = TestAutomata.getPosition();
            TestAutomata.SetPosition(rand);
            Assert.AreNotEqual(PositionBefore, TestAutomata.getPosition());
            TestAutomata.AddState(rand);
            Assert.AreEqual(5, TestAutomata.getStates());
            Assert.AreEqual(5,TestAutomata.GetTransitionTable(0).getSize());
            Assert.AreEqual(5, TestAutomata.GetTransitionTable(1).getSize());
            double error = 0.12;
            double targeterror;
            TestAutomata.SetError(error);
            targeterror = TestAutomata.getError();
            Assert.AreEqual(error, targeterror);
        }
        [TestMethod]
        public void TransitionTableTest()
        {
            Random rand = new Random();
            TransitionTable target = new TransitionTable(4, rand);
            int count = 0;
            double[,] arr = target.GetTransitionMatrix();
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (arr[i, j] == 1)
                        count++;
            Assert.AreEqual(4, count);
        }

        [TestMethod]
        public void AutomataVelocityTest()
        {
            double[,] tmp1 = new double[4, 4] { { 0, 0, 0, 0 }, { 1, 0, 0, 0 }, { 0, 0, 1, 0 }, { 0, 1, 0, 1 } };
            double[,] tmp2 = new double[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 1, 0, 1, 0 }, { 0, 1, 0, 1 } };
            List<TransitionTable> SampleTable = new List<TransitionTable>();
            SampleTable.Add(new TransitionTable(4, tmp1));
            SampleTable.Add(new TransitionTable(4, tmp2));
            Random rand = new Random();
            int[] alphabet = { 0, 1 };
            Automata TestAutomata = new Automata(4, alphabet, SampleTable, -1);
            List<Word[]> samplelist = WordGenerator.GenerateWords(alphabet, nrLetters, rand, 30, LengthOfWordsFrom, LengthOfWordsTo, constant);

            double[] VelocityBefore = TestAutomata.getVelocity();
            Neighbours N = new Neighbours(-1, 2, 3, 4);
            Automata TestAutomata1 = new Automata(4, alphabet, SampleTable, 2);
            Automata TestAutomata2 = new Automata(4, alphabet, SampleTable, 3);
            Automata TestAutomata3 = new Automata(4, alphabet, SampleTable, 4);
            List<Automata> list = new List<Automata>();
            list.Add(TestAutomata);
            list.Add(TestAutomata1);
            list.Add(TestAutomata2);
            list.Add(TestAutomata3);

            TestAutomata.ComputeAutomata(samplelist[0]);
            TestAutomata1.ComputeAutomata(samplelist[0]);
            TestAutomata2.ComputeAutomata(samplelist[0]);
            TestAutomata3.ComputeAutomata(samplelist[0]);
            TestAutomata.calculatevelocity(N, list, rand, TestAutomata3);
            Assert.AreNotEqual(TestAutomata.getVelocity(), VelocityBefore);
        }

    }
}
