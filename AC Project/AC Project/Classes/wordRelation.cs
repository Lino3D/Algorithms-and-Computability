using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Project.Classes
{
    //this class contains world related and through what character are they related.
    public class wordRelation
    {
        int EndingState { get; set; }
        List<Word> relatedWords { get; set; }


        public wordRelation(int estate, List<Word> words)
    {
        EndingState = estate;
        relatedWords = words;

    }
        public int getEndingState()
        { return this.EndingState; }
        public List<Word> getRelatedWords()
        { return this.relatedWords; }
    }
}
