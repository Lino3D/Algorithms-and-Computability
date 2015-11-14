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

       void CreateAutomata(int s, int[] alphabet, List<TransitionTable> _transitiontables)
        {
            States=s;
            Alphabet = alphabet;
            TransitionTables = _transitiontables;
        }

    }
}
