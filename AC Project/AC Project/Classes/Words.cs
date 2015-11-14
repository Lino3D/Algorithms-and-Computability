using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Project.Classes
{
    class Words
    {
        int[][] Words;
        int NumWords;
        int NumAccepted;
        int NumRejected;
        public Words(int[][] words, int numwords)
        {
            NumWords = numwords;
            for( int i = 0; i < NumWords; i++)
                Array.Copy(words[i], Words[i], words[i].Count());
        }

        public int GetWordsNum()
        {
            return NumWords;
        }

        public int[] GetWord(int i)
        {
            return Words[i];
        }
    }
}
