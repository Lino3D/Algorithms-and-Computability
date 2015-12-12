using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Project.Classes
{
    /* Class responsible for keeping the Transition Tables
     * in form of a 2-dimensional array of doubles.
     * */
    public class TransitionTable
    {
        int size
        {
            get;
            set;
        }
        double[,] Table;
        /* Returns the size of the array */
        public int getSize()
        {
            return size;
        }

        /*Creation of the transition Matrix in the following pattern
         * T[x,y] - where x defines row and y defines column
         * i.e.
         * 1 0 0 0
         * 0 1 0 0
         * 0 0 1 0
         * 1 0 0 0
         * */
       public TransitionTable(int Size, Random rand)
        {
      //      TransitionTable trans = new TransitionTable();
       //     trans.size = size;
           // rand = new Random(DateTime.Now.Millisecond);
            size = Size;
            Table = new double[size, size];
            for( int i = 0; i < size; i++)
            {
                int r = rand.Next(size);
                for (int j = 0; j < size; j++)
                    if (j != r)
                        Table[j, i] = 0;
                    else
                        Table[j, i] = 1;
            }
      //      trans.Table = Table;
    
           // return trans;
        }
        /* Copy constructor - it takes the size and the table */
        public TransitionTable(int _size, double[,] _table)
       {
           this.size = _size;
           Table = _table;
       }
        /* Returns the Transition Matrix in form of
         * a 2-dimensional double arrray */
       public double[,] GetTransitionMatrix()
        {
            return Table;
        }

    }
}
