using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Project.Classes
{
    //this class contains world related and through what character are they related.
    //it keeps the EndingState of the word - particularly needed in the Automata.ComputeAutomata
    public class wordRelation
    {
        /* Gets and Sets the Ending State of
         * the word.
         * */
        int EndingState { get; set; }
        /* Keeps the list of related words.
         * 1 means they are related
         * 0 means they are not related
         * */
        List<Word> relatedWords { get; set; }

        /* Not used really */
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
