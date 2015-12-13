﻿using System;
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
        public static Word[] GenerateWords(int[] alphabet, int lettercount, Random rand, int NumOfWords, int LengthFrom, int LengthTo, int constant)
        {


             Word[] words = new Word[NumOfWords];
            Word[] wordsOld = GenerateWordsWithConstant(alphabet, lettercount, rand, constant);
            if (wordsOld.Count() > words.Count())
                words = new Word[wordsOld.Count()];

          for(int i=0; i<wordsOld.Count(); i++)
          {
              words[i] = wordsOld[i];

          }
            LengthFrom = constant;
            int StepSize = (LengthTo-LengthFrom)/9;
            int [] tmpWord;
            int c;
            int MaxLetters = LengthFrom;
            int count = 0;

            for (int i = wordsOld.Count(); i < NumOfWords; i++ )
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
        public static Word[] GenerateTestWords(int[] alphabet, int lettercount, Random rand, int NumOfWords, int LengthFrom, int LengthTo, Word[] TestSet)
        {
            Word[] words = new Word[NumOfWords];
            if (LengthFrom > LengthTo)
                LengthTo = LengthTo + LengthFrom;
            List<Word> WordsList = TestSet.ToList();
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
