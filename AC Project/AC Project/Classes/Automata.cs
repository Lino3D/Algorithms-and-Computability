using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Project.Classes
{
    public class Automata
    {
        int States {get; set;}
        int[] Alphabet;
        List<TransitionTable> TransitionTables;
        int[] Position;
        int id;
        double Error;
        int[] Relations = new int[4950];
        double[] Velocity;
        


       public Automata(int s, int[] alphabet, List<TransitionTable> _transitiontables, int _id)
        {
            States = s;
            Alphabet = alphabet;
            TransitionTables = _transitiontables;
            id = _id;
            calculateposition();
            Velocity = new double[Position.Count()];
            Array.Copy(Position, Velocity, Position.Count());
            
        }
        public int[] GetRelations()
        {
            return Relations;
        }
        public void AddState(Random rand)
        {
            States++;
            TransitionTables.Clear();
            TransitionTable tmp;
            for (int i = 0; i < Alphabet.Count(); i++)
            {
                tmp = new TransitionTable(States, rand);
                TransitionTables.Add(tmp);
            }

        }
        public void SetError(double _error)
        {
            this.Error = _error;
        }
        public double getError()
        {
            return Error;
        }
     public int GetId()
     {
         return id;
     }
        public void calculatevelocity(Neighbours N, List<Automata> automatas, Random r, int[] position)
     {
         int size = States * States * Alphabet.Length;
            double[] gx = calculategx(N, automatas,r);
            double[] px = calculatepx(position, r);
            double[] tmpV = new double[size];
            

         for (int i = 0; i < size; i++ )
         {
             
             tmpV[i] = this.getError() * Velocity[i] + gx[i] + px[i];
         }
         Velocity = tmpV;
          
     }
        
       public double[] calculategx(Neighbours N, List<Automata> automatas, Random r)
        {
            double[] a= new double[Position.Count()];
            int[] b = new int[Position.Count()];
            int[] wektorlosowy = new int[Position.Count()];
            int best = N.GetLocalBest();
            for (int i = 0; i < Position.Count(); i++ )
            {
                b[i] = this.getPosition()[i] - automatas[best].getPosition()[i];
            }
            for (int i = 0; i < Position.Count(); i++ )
            {
                wektorlosowy[i] = r.Next(2);
            }

            for (int i = 0; i < a.Count(); i++)
            {
                a[i] = (double) b[i] * (double) wektorlosowy[i];
            }
                return a;
        }
       public double[] calculatepx(int[] position, Random r)
       {
           double[] a = new double[Position.Count()];
           int[] wektorlosowy = new int[Position.Count()];
           for (int i = 0; i < Position.Count(); i++)
           {
               a[i] = this.getPosition()[i] - position[i];
           }
           for (int i = 0; i < Position.Count(); i++)
           {
               wektorlosowy[i] = r.Next(2);
           }

           for (int i = 0; i < a.Count(); i++)
           {
               a[i] = a[i] * wektorlosowy[i];
           }
           return a;
       }

        public void calculateposition()
        {
            List<TransitionTable>_transitiontables = TransitionTables;
            int size = States * States * Alphabet.Length;
            int[] _position = new int[size];
            for (int z = 0; z < Alphabet.Length; z++ )
            {
                double[,] tmp = _transitiontables[z].GetTransitionMatrix(); //get matrix
                for (int i = 0; i < tmp.Length/States ; i++)
                {
                    for (int j = 0; j < tmp.Length/States; j++)
                    {
                        _position[(z*States*States) + i * States + j ] = (int)tmp[j, i];
                    }
                }
            }
            this.Position = _position;
        }
        

        
        //The function takes the alphabet letter expressed by
        //an integer, to get the proper transition table
       public TransitionTable GetTransitionTable( int i )
        {
            return TransitionTables.ElementAt(i);
        }
       public int getStates()
        { 
            return States;
        }
       public int getAlphabetSize()
       {
           return Alphabet.Count();
       }
        public int[] getPosition()
       {
           return Position;
       }

     
        public static Automata GenerateParticle(int s, int[] alphabet, int _id, Random rand)
        {
            TransitionTable tmp;
            List<TransitionTable> ListOfTransitionTables = new List<TransitionTable>();
            for (int i = 0; i < alphabet.Count(); i++)
            {
                tmp = new TransitionTable(s, rand);
                ListOfTransitionTables.Add(tmp);
            }
            Automata automata = new Automata(s, alphabet, ListOfTransitionTables, _id);
            return automata;
        }
        /*
         * The function returns number of accepted words by the
         * Automata over the Words table
         * 
         * 
         * BASIC SKETCH OF THE FUNCTION
         */
        public int[] ComputeAutomata( Word[] words)
        {
           // int Count = 0;
            int[] Word;
            int[] FinalState = new int[words.Length];
            double[,] Table;
            int NextState = 0;
            int CurrentState = 0;
            for (int i = 0; i < words.Length; i++)
            {
                Word = words[i].getWord();
                CurrentState = 0;
                foreach (int Letter in Word)
                {
                    Table = this.GetTransitionTable(Letter).GetTransitionMatrix();
                    for (int j = 0; j < this.States; j++)
                        if (Table[j, CurrentState] != 0)
                        {
                            NextState = j;
                            break;
                        }
                    CurrentState = NextState;
                }
                FinalState[i] = CurrentState;
            }
            CalculateRelationsVector(FinalState);
            return FinalState;
        }

        //calculating Relations object in automata class.
        void CalculateRelationsVector(int[] final)
        {
            int count = 0;
            for( int i = 0 ; i < 100; i++)
            {
                for (int j = i + 1; j < 100; j++)
                {
                    if (final[i] == final[j])
                        Relations[count] = 1;
                    else
                        Relations[count] = 0;
                    count++;
                }
            }
            return;

        }
    }
}
