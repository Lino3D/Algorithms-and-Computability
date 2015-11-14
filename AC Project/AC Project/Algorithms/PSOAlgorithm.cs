using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC_Project.Classes;

namespace AC_Project.Algorithms
{
    class PSOAlgorithm
    {
        List<Automata> automatas = new List<Automata>();
        int n;
        /*
         * Where Automata ideal - searched problem
         * alphabet - alphabet of the automaton
         * n - number of particles
         * */
        public void ComputePSO( Automata ideal, int[] alphabet, int _n )
        {
            /* 1. Generate Particles, Velocity
             * 2. Compute Error and Choose Local Bests
             * 3. Choose Global Best
             * 
             * 
             */
            n = _n;
            for( int i = 0 ; i < n; i++)
            {
                //Generate 2-states random Automatons
                automatas.Add(Automata.GenerateParticle(2, alphabet));
            }
            int iterations = 0;
            Words words = WordGenerator.GenerateWords(alphabet, alphabet.Count());
            while( iterations < 500 )// && Error > MinError)
            {


                iterations++;
            }

        }
        int CalculateDistance(Automata a, Automata b)
    {
        int distance = 0;
        int dimensions = a.getStates() * a.getAlphabetSize() * a.getAlphabetSize();
       



            return distance;
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
