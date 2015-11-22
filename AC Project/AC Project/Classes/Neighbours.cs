using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Project.Classes
{
    public class Neighbours
    {
        List<int> Group = new List<int>();
        int LocalBest { get; set; }
        List<double> Error = new List<double>();

        public Neighbours(int a, int b, int c, int d)
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

      public  void SetLocalBest(int a)
        { LocalBest = a; }
        public int GetLocalBest(int b)
        { return LocalBest; }

    }
}
