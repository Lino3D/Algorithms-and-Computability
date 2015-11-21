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
        public static void ComputePSO( Automata ideal, int[] alphabet, int _n )
        {
            /* 1. Generate Particles, Velocity
             * 2. Compute Error and Choose Local Bests
             * 3. Choose Global Best
             * 4. Aplly velocity with previous step
             * 
             */
            int n;
            List<Automata> automatas = new List<Automata>();
            n = _n;
    //        for( int i = 0 ; i < n; i++)
    //        {
                //Generate 2-states random Automatons
    //            automatas.Add(Automata.GenerateParticle(2, alphabet));
   //         }
       //     ChooseLocalBests();
     //       for (int j = 0; j < n; j++ )
       //         automatas[j].AddState();
     //       ChooseLocalBests();
            int iterations = 0;
          //  Words words = WordGenerator.GenerateWords(alphabet, alphabet.Count());
       //     Word[] words = WordGenerator.GenerateWords(alphabet, alphabet.Count());
            while( iterations < 500 )// && Error > MinError)
            {
                iterations++;
            }
        }
        public static int CalculateDistance(Automata a, Automata b)
        {
            int distance = 0;
            int dimensions = a.getStates() * a.getAlphabetSize() * a.getAlphabetSize();
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

        public static void ChooseLocalBests(List<Automata> automatas, List<LocalBest> LocalBests, int n)
        {
            LocalBests.Clear();
            GroupAutomatas(automatas, LocalBests, n);

        }

        public static void GroupAutomatas(List<Automata> automatas, List<LocalBest> LocalBests, int n)
        {
            int MinDistance = Int32.MaxValue;
            int x = -1;
            int y = -1;
            int z = -1;
            int distance;
            int groupcount = 0;
            int[] Taken = new int[n];
            for (int i = 0; i < n && groupcount < 25; i++)
                Taken[i] = 0;


            for (int i = 0; i < n; i++)
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
                    LocalBest tmp = new LocalBest(i, x, y, z);
                    LocalBests.Add(tmp);
                    groupcount++;
                }
            }

        }
        public static List<wordRelation> Relations(int[] EndingStates, Word[] words)
        {
            //

            List<int> clusters = new List<int>(); // Holds information about number of possible relations between words.
            List<List<Word>> clustered = new List<List<Word>>();
            List<wordRelation> clustered2 = new List<wordRelation>();

            for (int i = 0; i < EndingStates.Length; i++)
            {
                if (clusters.Contains(EndingStates[i]) == false)
                { clusters.Add(EndingStates[i]); }
            }
            for (int i = 0; i < clusters.Count(); i++)
            {
                clustered.Add(new List<Word>());
                for (int j = 0; j < EndingStates.Count(); j++)
                {
                    if (EndingStates[j] == clusters[i])
                    {
                        clustered[i].Add(words[j]);
                        //  clustered2.Set
                    }
                   
                }
                clustered2.Add(new wordRelation(clusters[i], clustered[i]));
            
            }
            return clustered2;
        }
       public static double CalculateError(List<wordRelation> ToolRelated, List<wordRelation> ParticleRelated, int n )
        {
          //int[] errors = new int [n];

           List<int> Correct= new List<int>();
           List<Word> wrongwords = new List<Word>();
           List<Word> Correctwords = new List<Word>();
           for (int i = 0; i < ToolRelated.Count; i++ )
           {
               int EndingState = ToolRelated[i].getEndingState();
               List<Word> toolRelated = ToolRelated[i].getRelatedWords();

               for(int j=0; j<ParticleRelated.Count; j++)
               {
                   if (EndingState == ParticleRelated[j].getEndingState())
                   {
                       List<Word> particleRelated = ParticleRelated[j].getRelatedWords();
                 
                       for (int z = 0; z < toolRelated.Count; z++)
                       {
                           if (particleRelated.Contains(toolRelated[z]) == true)
                           {
                               Correct.Add(toolRelated[z].getId());
                               Correctwords.Add(toolRelated[z]);

                           }
                           else
                               wrongwords.Add(toolRelated[z]);
                       }
                   }

               }
             
           }
           if (wrongwords.Count + Correctwords.Count < n)
           {
               for (int i = 0; i < ToolRelated.Count; i++)
               {
                   List<Word> toolRelated = ToolRelated[i].getRelatedWords();

                   for (int j = 0; j < toolRelated.Count; j++)
                       if (Correctwords.Contains(toolRelated[j]) == false)
                           wrongwords.Add(toolRelated[j]);

               }
           }
               return 1-Correctwords.Count/100;
        }

       public static double CalculateError2(List<wordRelation> ToolRelated, List<wordRelation> ParticleRelated, int n)
       {
           int[] MarkedTable = new int[n];
           for (int i = 0; i < n; i++)
               MarkedTable[i] = 0;
           int StateComputed;
           foreach (var c in ParticleRelated)
           {
               MarkedTable[c.getRelatedWords()[0].getId()] = 1;
       //        StateComputed = c.getEndingState();

               //Recursion begins!!!!
                foreach( var ToolVar in ToolRelated)
                {
                    StateComputed = ToolVar.getEndingState();
                    foreach( var word in c.getRelatedWords())
                    {
                        if (ToolVar.getRelatedWords().Contains(word))
                            MarkedTable[word.getId()] = 1;
                    }
                }

          }

           return 0.0;
       }

       public static int CalculateRelations(Automata ideal, Automata particle)
       {
           int error = 0;
           int[] RelIdeal = ideal.GetRelations();
           int[] RelPart = particle.GetRelations();
           for (int i = 0; i < 4950; i++)
               if (RelIdeal[i] != RelPart[i])
                   error++;
           error = 4950 - error;
           return error;
       }

       public static void CalculateError(Automata ideal, List<Automata> automatas)
       {
           int error;
           foreach( var tmp in automatas)
           {
               error = CalculateRelations(ideal, tmp);
               tmp.SetError(error);
           }

       }
    /*    int ChooseLocalBest()
        {
            int best = 0;
            float ErrorMin = float.MaxValue;
            for( int i = 0; i < n ; i++)
            {
               

            }
            return best;
        }*/
    }
}
