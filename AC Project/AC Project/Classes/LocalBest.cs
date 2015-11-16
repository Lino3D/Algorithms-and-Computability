using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Project.Classes
{
    public class LocalBest
    {
        List<int> Group;
        int _LocalBest;
        List<int> Error;

        LocalBest ( Automata a, Automata b, Automata c, int aa, int bb, int cc)
        {
            Group.Add(aa);
            Group.Add(bb);
            Group.Add(cc);

        }
        public LocalBest(int aa, int bb, int cc)
        {
            Group = new List<int>();
            Group.Add(aa);
            Group.Add(bb);
            Group.Add(cc);

        }
        public List<int> GetGroup()
        {
            return Group;
        }
    }
}
