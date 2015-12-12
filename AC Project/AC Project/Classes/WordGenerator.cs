using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AC_Project.Classes
{
    public class WordGenerator
    {
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
                    var temporary = new Word(i, MaxLetters, tmpWord);
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

      

    }
}
