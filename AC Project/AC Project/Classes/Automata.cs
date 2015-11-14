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
    }
}
