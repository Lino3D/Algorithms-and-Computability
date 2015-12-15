using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC_Project.Classes;

namespace AC_Project.Algorithms
{
   public static class PSOAlgorithm
    {
      //  List<Automata> automatas = new List<Automata>();
      //  int n;
    //    List<LocalBest> LocalBests = new List<LocalBest>();

        /* The idea behind local bests is that we look
         * for the closes to each other automatas and group
         * them by 3. The choosing parameter is the disance in space
         * between them
         */
        
        /*
         * Where Automata ideal - searched problem
         * alphabet - alphabet of the automaton
         * n - number of particles
         * */
       public static Automata ComputePSO(Automata ideal, List<Automata> automatas, int[] alphabet, int n, Word[] words,
           List<Neighbours> Neighbours, Random rand, int MaxIterations, double acceptedError)
       {
           /* 1. Generate Particles, Velocity
            * 2. Compute Error and Choose Local Bests
            * 3. Choose Global Best
            * 4. Aplly velocity with previous step
            * 
            */
           Automata GlobalBest = automatas[0];
           Automata BestFound = automatas[0]; 

           for (int states = 0; states < 10; states++)
           {
               GlobalBest = automatas[0];

               History history = new History();
            

               int iterations = 0;
               while (iterations < MaxIterations)
               {
                   ideal.ComputeAutomata(words);

                   foreach (Automata AT in automatas)
                       AT.ComputeAutomata(words);
                   int id2;
                   PSOAlgorithm.CalculateError(ideal, automatas);
                   List<double> abc = new List<double>();
                   foreach (var c in automatas)
                       if (!abc.Contains(c.getError())){
                           abc.Add(c.getError());
                           if (c.getError() == 0.0)
                               id2 = c.GetId();
                       }
                   Neighbours = PSOAlgorithm.ChooseLocalBests(automatas, Neighbours, n);
                   GlobalBest = PSOAlgorithm.FindGlobalBest(Neighbours, automatas);
                   history.AddGlobalBest(GlobalBest);
                   GlobalBest = history.ReturnBestAutomata(GlobalBest, automatas[0].getStates());
                   automatas[GlobalBest.GetId()] = GlobalBest;

                   foreach (var item in Neighbours){
                           List<int> Group;
                           Group = item.GetGroup();
                           foreach (var item2 in Group)
                               if (item2 != GlobalBest.GetId()) //not Global
                                   automatas[item2].calculatevelocity(item, automatas, rand, GlobalBest);
                   }

                   foreach (var automata in automatas)
                       if (automata != GlobalBest) //Not global
                           automata.SetPosition(rand);
                 
                   iterations++;
                   if (GlobalBest.getError() < acceptedError)
                       break;
               }




               if (states == 0)
                   BestFound = new Automata(GlobalBest.getStates(), alphabet, GlobalBest.GetTransitionTables(), GlobalBest.GetId(), GlobalBest.getError(), GlobalBest.GetRelations());
               else
                   if (BestFound.getError() > GlobalBest.getError())
                       BestFound = new Automata(GlobalBest.getStates(), alphabet, GlobalBest.GetTransitionTables(), GlobalBest.GetId(), GlobalBest.getError(), GlobalBest.GetRelations());

               if (BestFound.getError() < acceptedError)
                   break;
               foreach (var c in automatas)
                   c.AddState(rand);

           }



           double error = CalculateRelations(ideal, BestFound);


        //   GlobalBest = history.ReturnBestAutomata(GlobalBest);
           return BestFound;
       }
        public static int CalculateDistance(Automata a, Automata b)
        {
            int distance = 0;
            int dimensions = a.getStates() * a.getStates() * a.getAlphabetSize();
            int[] Difference = new int[dimensions];
            int[] x, y;

            a.calculateposition();
            x = a.getPosition();
            b.calculateposition();
            y = b.getPosition();
            for( int i = 0; i < dimensions; i++)
            {
                if (x[i] != y[i])
                    distance++;
            }

                return distance;
        }

        public static Automata FindGlobalBest(List<Neighbours> N, List<Automata> automatas)
        {
            double MinValue = double.MaxValue;
            int id= 0;
            foreach( var item in N)
            {
                if( item.GetLocalMinError() < MinValue)
                {
                    MinValue = item.GetLocalMinError();
                    id = item.GetLocalBest();
                }
            }
            return automatas.ElementAt(id);
        }
               /*Function choosese localbest in each neighbourhood based on error of each automata in every neighbourhood. 
         * Automata with the smallest error is the new localbest.
         * All other automatas will try to move towards in in next iteration.
         * 
         * 
  **/

        public static List<Neighbours> ChooseLocalBests(List<Automata> automatas, List<Neighbours> Neighbours, int n)
        {
            Neighbours.Clear();
           Neighbours= GroupAutomatas(automatas, Neighbours, n);
                
           foreach(Neighbours N in Neighbours )
           {
               
               List<int> Neighbourhood = N.GetGroup(); //This list collects indexes of automatas
               int _localbest = Neighbourhood[0];

               foreach (int index in Neighbourhood)
               {
                   if (automatas[index].getError() < automatas[_localbest].getError())
                       _localbest = automatas[index].GetId();
               }

               N.SetLocalBest(_localbest, automatas[_localbest].getError());
           }

           return Neighbours;
        }

        /* This function groups automatas closest to each other
         * It checks position of each automata.
         * It creats a neighbourhood -> 4 automatas with most similar position.
         * 
        * 
        * */

        public static List<Neighbours> GroupAutomatas(List<Automata> automatas, List<Neighbours> Neighbours, int n)
        {
            int MinDistance = Int32.MaxValue;
            int x = -1;
            int y = -1;
            int z = -1;
            int distance;
            int groupcount = 0;
            int[] Taken = new int[n];
            for (int i = 0; i < n ; i++)
                Taken[i] = 0;


            for (int i = 0; i < n && groupcount < 25; i++)
            {
                if (Taken[i] != 1)
                {
                    Taken[i] = 1;
                    MinDistance = Int32.MaxValue;
                    for (int j = i; j < n; j++)
                    {
                        if (Taken[j] != 1)
                        {
                            distance = CalculateDistance(automatas[i], automatas[j]);
                            if (distance < MinDistance)
                            {
                                MinDistance = distance;
                                x = j;
                            }
                        }
                    }
                    Taken[x] = 1;
                    MinDistance = Int32.MaxValue;
                    for (int j = i; j < n; j++)
                    {
                        if (Taken[j] != 1)
                        {
                            distance = CalculateDistance(automatas[i], automatas[j]);
                            if (distance < MinDistance)
                            {
                                MinDistance = distance;
                                y = j;
                            }
                        }
                    }
                    Taken[y] = 1;

                    MinDistance = Int32.MaxValue;
                    for (int j = i; j < n; j++)
                    {
                        if (Taken[j] != 1)
                        {
                            distance = CalculateDistance(automatas[i], automatas[j]);
                            if (distance < MinDistance)
                            {
                                MinDistance = distance;
                                z = j;
                            }
                        }
                    }
                    Taken[z] = 1;
                    Neighbours tmp = new Neighbours(i, x, y, z);
                    Neighbours.Add(tmp);
                    groupcount++;
                }
            }
            return Neighbours;
        }



       /*This function calculates the error based on word relation.
        * It uses Humming distance to calculate the difference in relations between ideal and particle automatons
        * Error itself is the number differencets divided by overall number of pairs of words.
        * 
        * */

       public static double CalculateRelations(Automata ideal, Automata particle)
       {
           int error = 0;
       
           int[] RelIdeal = ideal.GetRelations();
           int[] RelPart = particle.GetRelations();
           for (int i = 0; i < RelIdeal.Count(); i++)
           {
               if (RelIdeal[i] != RelPart[i])
                   error++;
           }

           return ((double)error / (double) RelIdeal.Count());
       }



       public static void CalculateError(Automata ideal, List<Automata> automatas)
       {
           double error;
           foreach( var tmp in automatas)
           {
               error = CalculateRelations(ideal, tmp);
               tmp.SetError(error);
           }

       }
    }
}
