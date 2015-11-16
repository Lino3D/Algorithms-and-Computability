using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Project.Classes
{
    public class Words
    {
        public int[][] _Words;
        int NumWords = 100;
        int NumAccepted;
        int NumRejected;
        public Words(int[][] words, int numwords)
        {
            int length = 10;
            _Words = new int[100][];
            for (int i = 0; i < 100; i++ )
            {
                if (i != 0 && i % 10 == 0)
                    length += 10;
                _Words[i] = new int[length];

            }
                NumWords = numwords;
            for( int i = 0; i < NumWords; i++)
                Array.Copy(words[i], _Words[i], words[i].Count());
        }

        public int GetWordsNum()
        {
            return NumWords;
        }

        public int[] GetWord(int i)
        {
            return _Words[i];
        }
    }
}
