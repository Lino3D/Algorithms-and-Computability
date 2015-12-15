using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC_Project.Classes;

namespace AC_Project.Classes
{
    /* The class focuses on storing the history of the Automatas
     * It is not a static class, so the object must be stored in
     * the PSO function.
     * */
    public class History
    {
        List<Automata> BestAutomatas = new List<Automata>();

        /* Add Global Best to the history 
         * The function takes as a parameter an Automata with GlobalBest
         */
        public void AddGlobalBest(Automata GlobalBest)
        {
            int[] alphabet = new int[GlobalBest.getAlphabet().Count()];
            List<TransitionTable> list= new List<TransitionTable>();
            Array.Copy(GlobalBest.getAlphabet(), alphabet, GlobalBest.getAlphabet().Count());
            foreach (var item in GlobalBest.GetTransitionTables())
            {
                list.Add(item);
            }
            Automata tmp2 = new Automata(GlobalBest.getStates(), alphabet, list, GlobalBest.GetId(), GlobalBest.getError(), GlobalBest.GetRelations());
            BestAutomatas.Add(tmp2);
        }
        /* Function returns the best automata. It takes into consideration
         * the current number of states (int states) and the Current Global Best
         * Automata (Automata CurrentBest)
         * */
        public Automata ReturnBestAutomata(Automata CurrentBest, int states)
        {
            Automata Best = CurrentBest;
            if (BestAutomatas.Count == 0)
                return Best;
           
            for (int i = 0; i < BestAutomatas.Count; i++ )
                if (BestAutomatas[i].getError() < Best.getError() && BestAutomatas[i].getStates() == states)
                    Best = BestAutomatas[i];
            
            return Best;
        }

        public void Clear()
        {
            BestAutomatas.Clear();
        }

    }
}
