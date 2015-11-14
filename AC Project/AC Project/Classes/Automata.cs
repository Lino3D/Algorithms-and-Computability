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
        int[] Position;




       public Automata(int s, int[] alphabet, List<TransitionTable> _transitiontables)
        {
            States = s;
            Alphabet = alphabet;
            TransitionTables = _transitiontables;
            
        }
        public void calculateposition(List<TransitionTable>_transitiontables)
        {
            int size = States * States * Alphabet.Length;
            int[] _position = new int[size];
            for (int z = 0; z < Alphabet.Length; z++ )
            {
                double[,] tmp = _transitiontables[z].GetTransitionMatrix(); //get matrix
                for (int i = 0; i < tmp.Length; i++)
                {
                    for (int j = 0; j < tmp.Length; j++)
                    {
                        //tmp[j, i] = (int)Char.GetNumericValue(line[i * states + j + states * states * z]);
                        _position[i * States + j + size] = (int)tmp[j, i];
                    }
                }
            }
            this.Position = _position;
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
            double[,] Table;
            int NextState = 0;
            int CurrentState = 0;
            for (int i = 0; i < words.GetWordsNum(); i++ )
            {
                Word = words.GetWord(i);

                foreach (int WordLoop in Word)
                {
                    Table = automata.GetTransitionTable(Word[WordLoop]).GetTransitionMatrix();
                    for (int j = 0; j < automata.States; j++)
                        if (Table[CurrentState,j] != 0)
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
