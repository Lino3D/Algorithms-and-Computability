using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AC_Project.Classes
{
    public class WordGenerator
    {
        public static Word[] GenerateWords(int[] alphabet, int lettercount, Random rand)
        {
         // int[][] Words = new int[100][];
            Word[] words = new Word[100];

            int StepSize = 9;
            int [] tmpWord;
            int c;
            int MaxLetters = 10;
            for (int i = 0; i < 100; i++ )
            {
                tmpWord = new int[MaxLetters];
                for (int j = 0; j < MaxLetters; j++)
                {
                    c = rand.Next(lettercount);
                    tmpWord[j] = alphabet[c];
                }
             //   Words[i] = tmpWord;
                words[i] = new Word(i, MaxLetters, tmpWord);
                if (i % 10 == 0 && i != 0)
                    MaxLetters += StepSize;
            }
            return words;
          //  Words tmp = new Words(Words, 100);
         //   return tmp;
        }

      

    }
}
