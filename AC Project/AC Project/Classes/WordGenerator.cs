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
        public static List<Word[]> GenerateWords(int[] alphabet, int lettercount, Random rand, int NumOfWords, int LengthFrom, int LengthTo, int constant)
        {


             Word[] wordsAll = new Word[NumOfWords];
            Word[] wordsConstant = GenerateWordsWithConstant(alphabet, lettercount, rand, constant);
            if (wordsConstant.Count() > wordsAll.Count())
            {
                wordsAll = new Word[wordsConstant.Count() + 100]; //at least 100 words more than constant created words, for practical reasons.
                NumOfWords = wordsAll.Count();
            }
            Word[] wordsgreaterthanC = new Word[wordsAll.Count()-wordsConstant.Count()];

          for(int i=0; i<wordsConstant.Count(); i++)
          {
              wordsAll[i] = wordsConstant[i];

          }
            LengthFrom = constant;
            int StepSize = (LengthTo-LengthFrom)/9;
            int [] tmpWord;
            int c;
            int MaxLetters = LengthFrom;
            int count = 0;

            for (int i = wordsConstant.Count(), z=0; i < NumOfWords; i++, z++ )
            {
                tmpWord = new int[MaxLetters];
                for (int j = 0; j < MaxLetters; j++)
                {
                    c = rand.Next(lettercount);
                    tmpWord[j] = alphabet[c];
                }
                wordsAll[i] = new Word(0, MaxLetters, tmpWord);
                wordsgreaterthanC[z] =  new Word(0, MaxLetters, tmpWord);
                      count++;
                if (StepSize == count)
                {
                    MaxLetters += StepSize;
                    count = 0;
                    if (LengthTo - MaxLetters < StepSize)
                        MaxLetters = LengthTo;
                }
            }
            List<Word[]> subsets = new List<Word[]>();
            subsets.Add(wordsAll);
            subsets.Add(wordsConstant);
            subsets.Add(wordsgreaterthanC);
            return subsets;
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
        public static Word[] GenerateTestWords(int[] alphabet, int lettercount, Random rand, int NumOfWords, int constant, int LengthTo, Word[] TrainingSet)
        {
            //Lenghtfrom is replaced with constant to match the reconstruction requirements
            if (constant > LengthTo)
                LengthTo = LengthTo + constant;
            List<Word> WordsList = TrainingSet.ToList();

            Word[] words = new Word[WordsList.Count()];

            int StepSize = (LengthTo - constant) / 9;
            int[] tmpWord;
            int c;
            int MaxLetters = constant;
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
                    if ( !IsThereThisWord( WordsList,tmpWord) )
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
            List<int[]> WordsList = new List<int[]>();
            
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
                    Word temporary = new Word(0, MaxLetters, tmpWord);

                    if (IsThereThisWord(WordsList,tmpWord))
                        Contains = true;
                    else
                        Contains = false;
                }
                Contains = true;

                WordsList.Add(tmpWord);
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

        public static bool IsThereThisWord(List<int[]> Arr, int[] word)
        {
            bool difference = false;
            foreach( var item in Arr)
            {
                if (item.Length == word.Length)
                {
                    for (int i = 0; i < item.Length; i++)
                        if (item[i] != word[i])
                            difference = true;

                    if (difference == false)
                        return true;
                }
                difference = false;
               


            }
            return false;

        }

        public static bool IsThereThisWord(List<Word> Arr, int[] word)
        {
            bool difference = false;
            foreach (var item in Arr)
            {
                if( item != null)
                if (item.getWord().Length == word.Length)
                {
                    for (int i = 0; i < item.getWord().Length; i++)
                        if (item.getWord()[i] != word[i])
                            difference = true;

                    if (difference == false)
                        return true;
                }
                difference = false;



            }
            return false;

        }

    }
}
