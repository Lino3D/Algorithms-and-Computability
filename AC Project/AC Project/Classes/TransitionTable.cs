using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Project.Classes
{
    public class TransitionTable
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
            int a = 1;
           // return trans;
        }
        // TO JEST ZJEBANE!!!! HEHEHEHEHEhe hueehuehuheuheuheuhe
        public void IncreaseSize( Random rand)
       {
            size++;
            int r;
            double[,] tmp = new double[size, size];
            for( int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    if ( j == size - 1)
                        tmp[i, j] = 0;
                    else if( i == size - 1)
                    {
                        r = rand.Next(size);
                        for (int k = 0; k < size; k++)
                            if (k != r)
                                tmp[i, k] = 0;
                            else
                                tmp[i, k] = 1;
                    }
                    else
                    tmp[i, j] = Table[i, j];
            }
            Table = tmp;
       }
        public TransitionTable(int _size, double[,] _table)
       {
           this.size = _size;
           Table = _table;
       }

       public double[,] GetTransitionMatrix()
        {
            return Table;
        }

    }
}
