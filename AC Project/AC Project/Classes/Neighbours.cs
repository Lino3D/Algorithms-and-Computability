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
        double MinError;
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

      public  void SetLocalBest(int id, double error)
        { LocalBest = id; MinError = error; }
        public int GetLocalBest()
        { return LocalBest; }

    }
}
