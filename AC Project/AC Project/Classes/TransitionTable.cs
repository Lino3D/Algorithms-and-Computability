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
       void createTable(int size)
        {
            Table = new double[size, size];
        }
    }
}
