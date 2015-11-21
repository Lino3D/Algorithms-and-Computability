using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC_Project.Classes;

namespace AC_Project.Classes
{
    class History
    {
        List<List<Automata>> AutomataHistory;
        List<int> StateHistory;

        public void AddToHistory(List<Automata> history)
        {
            AutomataHistory.Add(history);
            StateHistory.Add(history.First().getStates());
        }

        public int CompareAutomataInHistory(int id, double CurrentError)
        {
            //double MinEror = Double.MaxValue;
            int BestId = -1;
            for( int i = 0 ; i < AutomataHistory.Count(); i++)
            {
            //     if( AutomataHistory[i][id].GetError)
            }
            return BestId;
        }
    }
}
