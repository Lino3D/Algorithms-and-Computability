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

        }


    }
}
