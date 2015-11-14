using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Project.Classes
{
    class TransitionTable
    {
        int size
        {
            get;
            set;
        }
        double[,] Table;

        /*Creation of the transition Matrix in the following pattern
         * T[x,y] - where x defines row and y defines column
         * i.e.
         * 1 0 0 0
         * 0 1 0 0
         * 0 0 1 0
         * 1 0 0 0
         * */
       public TransitionTable(int Size)
        {
      //      TransitionTable trans = new TransitionTable();
       //     trans.size = size;
            size = Size;
            Random rand = new Random();
            Table = new double[size, size];
            for( int i = 0; i < size; i++)
            {
                int r = rand.Next(size);
                for (int j = 0; j < size; j++)
                    if (j != r)
                        Table[i, j] = 0;
                    else
                        Table[i, j] = 1;
            }
      //      trans.Table = Table;
           // return trans;
        }
        public TransitionTable(int _size, double[,] _table)
       {
           this.size = _size;
           Table = _table;
       }

    }
}
