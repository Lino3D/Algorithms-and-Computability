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






        [TestMethod]
        public void AutomataTest()
        public void WordsGenerationMethod1()
        {
            Word[] testWords = WordGenerator.GenerateWords(alphabet, nrLetters, rand, NumOfWords, LengthOfWordsFrom, LengthOfWordsTo);
            int actualCount = testWords.Count();

            //checking if number of words is good.
            Assert.AreEqual(100, actualCount);

            int index = 2;

            //checking if word index is correct;
            int actualID = testWords[index].getId();
            Assert.AreEqual(2, actualID);
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



        }
 
   


    }
}
