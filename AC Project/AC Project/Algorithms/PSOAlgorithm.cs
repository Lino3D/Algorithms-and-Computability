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
        List<LocalBest> LocalBests = new List<LocalBest>();

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
        public void ComputePSO( Automata ideal, int[] alphabet, int _n )
        {
            /* 1. Generate Particles, Velocity
             * 2. Compute Error and Choose Local Bests
             * 3. Choose Global Best
             * 4. Aplly velocity with previous step
             * 
             */
            n = _n;
            for( int i = 0 ; i < n; i++)
            {
                //Generate 2-states random Automatons
                automatas.Add(Automata.GenerateParticle(2, alphabet));
            }
            ChooseLocalBests();
            for (int j = 0; j < n; j++ )
                automatas[j].AddState();
            ChooseLocalBests();
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

        void ChooseLocalBests()
        {
            int MinDistance = Int32.MaxValue;
            int x = -1;
            int y = -1;
            int distance;
            int[] Taken = new int[n];
            for (int i = 0; i < n; i++)
                Taken[i] = 0;


            for( int i = 0; i < n; i++) {
                if( Taken[i] != 1){
                    Taken[i] = 1;
                    MinDistance = Int32.MaxValue;
                    for( int j = i; j < n; j++){
                        if(Taken[j] != 1){
                            distance = CalculateDistance(automatas[i], automatas[j]);
                            if( distance < MinDistance){
                                MinDistance = distance;
                                x = j;}}
                    }
                    Taken[x] = 1;
                    MinDistance = Int32.MaxValue;
                    for( int j = i; j < n; j++)
                    {
                        if(Taken[j] != 1){
                            distance = CalculateDistance(automatas[i], automatas[j]);
                            if( distance < MinDistance){
                                MinDistance = distance;
                                y = j;}}
                    }
                    Taken[y] = 1;
                   //UWAGA DODAJE -1 DO LOCAL BESTOW!!!!!!
                    LocalBest tmp = new LocalBest(i, x, y);
                    LocalBests.Add(tmp);
                }
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
