using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC_Project.Classes;

namespace AC_Project.Classes
{
    public class History
    {
        List<List<Automata>> AutomataHistory = new List<List<Automata>>();
        List<int> StateHistory= new List<int>();


        List<Automata> BestAutomatas = new List<Automata>();

        public void AddToHistory(List<Automata> history)
        {
            AutomataHistory.Add(history);
            StateHistory.Add(history.First().getStates());
        }

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

        public Automata ReturnBestAutomata(Automata CurrentBest)
        {
            Automata Best = CurrentBest;
            if (BestAutomatas.Count == 0)
                return Best;

            for (int i = 0; i < BestAutomatas.Count; i++)
                if (BestAutomatas[i].getError() < Best.getError())
                    Best = BestAutomatas[i];

            return Best;
        }

        public Automata ReturnLowestErrorAutomata(double CurrentError, int CurrentStates, Automata CurrentLowest)
        {
            //double MinEror = Double.MaxValue;
            double MinError = CurrentError;
            Automata MinimumAutomata = CurrentLowest;
            if (AutomataHistory == null)
                return MinimumAutomata;
            for( int i = 0 ; i < AutomataHistory.Count(); i++)
            {
                foreach (var item in AutomataHistory[i])
                {
                    if (item.getStates() == CurrentStates)
                    {
                        if (item.getError() < MinError)
                        {
                            MinError = item.getError();
                            MinimumAutomata = item;
                        }
                    }
                }
            }
            return MinimumAutomata;
        }
    }
}
