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

        //parameters for testing.
        double aError = 0.005;
        int NumOfWords = 100;
        int LengthOfWordsFrom = 9;
        int LengthOfWordsTo = 100;
        int MaxIterations = 500;
        int n = 100; 
        int[] alphabet = {0,1};
        int nrLetters = 2;
        Random rand = new Random();




        //test method for checking if word are generated correctly 

        [TestMethod]
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
        }
 
   


    }
}
