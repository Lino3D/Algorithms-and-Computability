using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Project.Classes
{
    public class Nieghbours
    {
        List<int> Group = new List<int>();
        int _LocalBest;
        List<int> Error;

        public Nieghbours(int a, int b, int c, int d)
        {
            Group.Add(a);
            Group.Add(b);
            Group.Add(c);
            Group.Add(d);
        }
        public List<int> GetGroup()
        {
            return Group;
        }
    }
}
