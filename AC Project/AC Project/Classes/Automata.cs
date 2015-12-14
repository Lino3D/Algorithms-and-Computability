using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Project.Classes
{
    /* This class is responsible for storing Automata
     * it saves the number of states, alphabet, Transition tables,
     * the position (generation of which is based on transition tables),
     * the automata's id, relations vector and the velocity vector
     * */
    public class Automata
    {
        int States {get; set;}
        int[] Alphabet;
        public List<TransitionTable> TransitionTables;
        int[] Position;
        int id;
        double Error;
        int[] Relations;
        double[] Velocity;

        /* Constructor of this class. It takes as the parameters:
         * int s - the number of states 
         * int[] alphabet - the actual alphabet
         * List<TransitionTable> _transitiontables - List of transition tables
         * int _id - the id of the automata
         * It simply constructs the Automata based on given input.
         * */
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
       /* Constructor of this class. It takes as the parameters:
        * int s - the number of states 
        * int[] alphabet - the actual alphabet
        * List<TransitionTable> _transitiontables - List of transition tables
        * int _id - the id of the automata
        * double error - error of the automata
        * int[] rel - relations vector of the automata
        * It simply constructs the Automata based on given input.
        * */
       public Automata(int s, int[] alphabet, List<TransitionTable> _transitiontables, int _id, double error, int[] rel)
       {
           States = s;
           Alphabet = alphabet;
           TransitionTables = _transitiontables;
           id = _id;
           calculateposition();
           Velocity = new double[Position.Count()];
           Array.Copy(Position, Velocity, Position.Count());
           Error = error;
           Relations = rel;

       }
        /* Returns the Relations Vector of the Automata */
        public int[] GetRelations()
        {
            return Relations;
        }
        /* Sets the position of the automata. It invokes the method
         * of velocity and composing it - creates a new position.
         * Then it updates the transition tables of the Automata
         * and it sets the position into the variable of the class.
         * As the parameter it takes the Random variable - it is 
         * need when passing into Dyskretyzacja method.
         * */
        public void SetPosition( Random rand)
        {
            List<TransitionTable> _transitiontables = new List<TransitionTable>();
            double[,] tmp;
            int size = States * States * Alphabet.Length;
            double[] _position = new double[size];

            for (int i = 0; i < size; i++)
                _position[i] = Velocity[i] + (double)Position[i];

            for (int i = 0; i < _position.Count(); i++)
                if (_position[i] < 0.0 )
                    _position[i] = 0.0;
                else
                    _position[i] = 1.0;

            for (int i = 0; i < _position.Count(); i++)
                Position[i] = (int) _position[i];

            //Wrzucenie w tabele przejscia
                for (int z = 0; z < Alphabet.Length; z++)
                {
                    tmp = new double[States, States];
                    for (int i = 0; i < States; i++)
                        for (int j = 0; j < States; j++)
                            tmp[j, i] = _position[(z * States * States) + i * States + j];

                    TransitionTable table = new TransitionTable(States, tmp);
                    _transitiontables.Add(table);
                }

                TransitionTables = _transitiontables;
                Dyskretyzacja( rand);
                calculateposition();
                

        }
        /* Funciton that takes care of the position vector/transition
         * tables to be proper discrete values. It deletes
         * any additional 1's if there are more than 1 in a row.
         * And the deletion is made by random. Also it places a rando
         * 1 if there are no 1's in the row
         * */
         public void Dyskretyzacja(Random rand)
        {


            foreach( var table in TransitionTables)
            {
                int countones = 1;
                int r;
                double[,] tmp =  table.GetTransitionMatrix();

                for (int i = 0; i < table.getSize(); i++)
                {
                    countones = 0;
                    for (int j = 0; j < table.getSize(); j++)
                        if (tmp[j, i] == 1)
                        {
                            countones++;
                        }

                    if (countones == 0)
                    {
                        r = rand.Next(table.getSize());
                        tmp[r, i] = 1;

                    }
                    else if (countones > 1)
                    {
                        for (int k = 0; k < table.getSize(); k++)
                            tmp[k, i] = 0;
                        r = rand.Next(table.getSize());
                        tmp[r, i] = 1;
                    }

                }


            }
        }
        /* Function Adds state to the Automaton.
         * To do that, a new transition tables need to
         * be chosen and set into the Automata.
         * */
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
            calculateposition();
            Velocity = new double[Position.Count()];
            Array.Copy(Position, Velocity, Position.Count());
            

        }

        /* Sets the error into the object. The function takes
         * single double value
         */
        public void SetError(double _error)
        {
            this.Error = _error;
        }
        /* Returns the error of the Automata */
        public double getError()
        {
            return Error;
        }
        /* Returns the Id of the Automata */
     public int GetId()
     {
         return id;
     }
        /* Calculate the Current Velocity for the Automaton. It takes into
         * consideration the previous velocity, position of the global best
         * and it's local best. Then using proper uniform distribution it
         * computes the current velocity.
         * Neighbours N - Neighbourhood of the particle object
         * List<Automata> automatas - List of all automatas in PSO
         * Random r - Random variable
         * Automata Globalbest - Global best object
         * */
        public void calculatevelocity(Neighbours N, List<Automata> automatas, Random r, Automata Globalbest)
     {
         int size = States * States * Alphabet.Length;
            double[] gx = calculategx(N, automatas,r);
            double[] px = calculatepx(Globalbest.getPosition(), r);
            double[] tmpV = new double[size];
            

         for (int i = 0; i < size; i++ )
         {

                 tmpV[i] = this.getError() * Velocity[i] + gx[i] + px[i];


         }
         Velocity = tmpV;
          
     }


       
        /* Calculates the difference between the position of the current Automata
         * and the Global Best Automata. Then it multiplies each value by some unifrom
         * distribution and returns the result as a vector.
         * Neighbours N - Neighbours object
         * List<Automata> automatas - List of Automatas
         * Random r - Random Variable
         * */
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
                wektorlosowy[i] = r.Next(5) - 2;
            }

            for (int i = 0; i < a.Count(); i++)
            {
                a[i] = ((double) b[i]+0.5) * (double) wektorlosowy[i];
            }
                return a;
        }
        /* Calculates the difference between the position of the current Automata
         * and the Local Best Automata. Then it multiplies each value by some unifrom
         * distribution and returns the result as a vector.
         * int[] position - current position of the Automata
         * Random r - Random Variable
         * */
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
               wektorlosowy[i] = r.Next(5) - 2;
           }

           for (int i = 0; i < a.Count(); i++)
           {
               a[i] = (a[i]+0.5) * wektorlosowy[i];
           }
           return a;
       }
        /* Function calculates the position of the Automaton.
         * It traverses the Transistion Tables of the Automaton
         * and sets them in to the Position vector.
         * */
        public void calculateposition()
        {
            List<TransitionTable>_transitiontables = TransitionTables;
            int size = States * States * Alphabet.Length;
            int[] _position = new int[size];
            for (int z = 0; z < Alphabet.Length; z++ )
            {
                double[,] tmp = _transitiontables[z].GetTransitionMatrix(); //get matrix
               // for (int i = 0; i < tmp.Length; i++)
                for (int i = 0; i < tmp.Length/States ; i++)
                {
                    for (int j = 0; j < tmp.Length/States; j++)
                    {
                        //tmp[j, i] = (int)Char.GetNumericValue(line[i * states + j + states * states * z]);
                     //   _position[i * States + j + size] = (int)tmp[j, i];
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
        /* The function returns the List of Transition Tables
         * of the Automaton
         * */
        public List<TransitionTable> GetTransitionTables()
       {
           return this.TransitionTables;
       }
        /* Returns the number of states */
       public int getStates()
        { 
            return States;
        }
        /* Returns the Alphabet size */
       public int getAlphabetSize()
       {
           return Alphabet.Count();
       }
        /* Returns the Position Vector of the Automaton */
        public int[] getPosition()
       {
           return Position;
       }

        /* Generates the Automata as a particle assigning it a unique id and returning
         * it to the function caller.
         * int s - the number of states
         * int[] alphabet - the alphabet
         * int _id - the unique id
         * Random rand - random variable
         * */
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
            CalculateRelationsVector(FinalState,words.Count());
            return FinalState;
        }


        //calculating Relations object in automata class.
        public void CalculateRelationsVector(int[] final, int NumberOfWords)
        {
            int count = 0;
            int tmp = ((NumberOfWords - 1) * NumberOfWords / 2);
            Relations = new int[tmp];
            for (int i = 0; i < NumberOfWords; i++)
            {
                for (int j = i + 1; j < NumberOfWords; j++)
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
        /* Returns the Alphabet as an array */
        public int[] getAlphabet()
        {
            return Alphabet;
        }
    }
}
