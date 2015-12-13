using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AC_Project.Classes
{

    public class WordGenerator
    {
        /* The function generates words and returns them as a form of array of Word object.
         * The parameters:
         * int[] alphabet - alphabet of the words
         * int lettercount - number of letters in the alphabet
         * Random rand - random variable
         * int NumOfWords - Number of words to be generated
         * int LengthFrom - Shortest words length
         * int LengthTo - Longest words length
         * */
        public static Word[] GenerateWords(int[] alphabet, int lettercount, Random rand, int NumOfWords, int LengthFrom, int LengthTo)
        {
            Word[] words = new Word[NumOfWords];

            int StepSize = (LengthTo-LengthFrom)/9;
            int [] tmpWord;
            int c;
            int MaxLetters = LengthFrom;
            int count = 0;

            for (int i = 0; i < words.Count(); i++ )
            {
                tmpWord = new int[MaxLetters];
                for (int j = 0; j < MaxLetters; j++)
                {
                    c = rand.Next(lettercount);
                    tmpWord[j] = alphabet[c];
                }
                words[i] = new Word(0, MaxLetters, tmpWord);
                count++;
                if (StepSize == count)
                {
                    MaxLetters += StepSize;
                    count = 0;
                    if (LengthTo - MaxLetters < StepSize)
                        MaxLetters = LengthTo;
                }
            }
            return words;
        }
        /* The function generates words and returns them as a form of array of Word object.
         * It takes into consideration another set of words - in order to create
         * an another disjoint set.
         * The parameters:
         * int[] alphabet - alphabet of the words
         * int lettercount - number of letters in the alphabet
         * Random rand - random variable
         * int NumOfWords - Number of words to be generated
         * int LengthFrom - Shortest words length
         * int LengthTo - Longest words length
         * Word[] TestSet - The set to be disjoint
         * */
        public static Word[] GenerateTrainingWords(int[] alphabet, int lettercount, Random rand, int NumOfWords, int LengthFrom, int LengthTo, Word[] TestSet)
        {
            Word[] words = new Word[NumOfWords];

            int StepSize = (LengthTo - LengthFrom) / 9;
            int[] tmpWord;
            int c;
            int MaxLetters = LengthFrom;
            int count = 0;
            bool Contains = true;
            for (int i = 0; i < words.Count(); i++)
            {
                tmpWord = new int[MaxLetters];

                while (Contains)
                {
                    for (int j = 0; j < MaxLetters; j++)
                    {
                        c = rand.Next(lettercount);
                        tmpWord[j] = alphabet[c];
                    }
                    var temporary = new Word(0, MaxLetters, tmpWord);
                    if (!TestSet.Contains(temporary))
                        Contains = false;
                }
                Contains = true;
                words[i] = new Word(i, MaxLetters, tmpWord);
                count++;
                if (StepSize == count)
                {
                    MaxLetters += StepSize;
                    count = 0;
                    if (LengthTo - MaxLetters < StepSize)
                        MaxLetters = LengthTo;
                }
            }
            return words;
        }






        public static Word[] GenerateWordsWithConstant(int[] alphabet, int lettercount, Random rand, int constant)
        {
            int NumberOfWords = 0;
            for ( int i = 0; i < constant; i++)
                NumberOfWords += (int) Math.Pow( (double) lettercount, (double)(i+1.0) );
            int[] tmpWord;
            Word[] words = new Word[NumberOfWords];
            int count = 0;

            int c;
            int MaxLetters = 1;
            int k = 1;
            int previous = 0;
            bool Contains = true;

            for (int i = 0; i < words.Count(); i++)
            {
                tmpWord = new int[MaxLetters];

                while (Contains)
                {
                    for (int j = 0; j < MaxLetters; j++)
                    {
                        c = rand.Next(lettercount);
                        tmpWord[j] = alphabet[c];
                    }
                    var temporary = new Word(0, MaxLetters, tmpWord);
                    if (!words.Contains(temporary))
                        Contains = false;
                }
                Contains = true;
                words[i] = new Word(0, MaxLetters, tmpWord);
                count++;
                if ( count == (int) Math.Pow( (double) lettercount, (double)(k))+ previous  )
                {
                    MaxLetters++;
                    previous = count;
                    k++;
                }
            }
            return words;

        }

      

    }
}
