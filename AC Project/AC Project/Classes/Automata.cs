using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Project.Classes
{
    class Automata
    {
        int States {get; set;}
        int[] Alphabet;
        List<TransitionTable> TransitionTables;

       public Automata(int s, int[] alphabet, List<TransitionTable> _transitiontables)
        {
            States = s;
            Alphabet = alphabet;
            TransitionTables = _transitiontables;
        }

        
        //The function takes the alphabet letter expressed by
        //an integer, to get the proper transition table
        TransitionTable GetTransitionTable( int i )
        {
            return TransitionTables.ElementAt(i);
        }
        public Automata GenerateParticle(int s, int[] alphabet)
        {
            TransitionTable tmp;
            List<TransitionTable> ListOfTransitionTables = new List<TransitionTable>();
            for (int i = 0; i < alphabet.Count(); i++)
            {
                tmp = new TransitionTable(s);
                ListOfTransitionTables.Add(tmp);
            }
            Automata automata = new Automata(s, alphabet, ListOfTransitionTables);
            return automata;
        }
        /*
         * The function returns number of accepted words by the
         * Automata over the Words table
         * 
         * 
         * BASIC SKETCH OF THE FUNCTION
         */
        public int ComputeAutomata(Automata automata, Words words)
        {
            int Count = 0;
            int[] Word;
            int[][] Table;
            int NextState = 0;
            int CurrentState = 0;
            for (int i = 0; i < words.GetWordsNum(); i++ )
            {
                Word = words.GetWord(i);

                foreach (int WordLoop in Word)
                {
                    Table = automata.GetTransitionTable(Word[WordLoop]);
                    for (int j = 0; j < automata.States; j++)
                        if (Table[CurrentState][j] != 0)
                        {
                            NextState = j;
                            break;
                        }
                    CurrentState = NextState;
                }
            }

                return Count;
        }
    }
}
