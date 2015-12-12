using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Project.Classes
{
    /* Class responsible for keeping the Neighbours.
     * It is essentially needed when it comes to the 
     * Local Best deals. And thus in calculating the 
     * Velocity in each particle.
     * */
    public class Neighbours
    {
        List<int> Group = new List<int>();
        int LocalBest { get; set; }
        double MinError;
        List<double> Error = new List<double>();
        
        /* Constructor of this class. Takes as arguments 
         * ids of the Automatas to be put in the neighbourhood.
         * */
        public Neighbours(int a, int b, int c, int d)
        {
            Group.Add(a);
            Group.Add(b);
            Group.Add(c);
            Group.Add(d);
        }
        /* Returns the List of the Automatas as in the
         * Neighbourhood. The List consists of the 4 elements.
         * */
        public List<int> GetGroup()
        {
            return Group;
        }
        /* Sets the local Best. As the argument takes the
         * id of the Local best and its error. Then it sets
         * the proper fields.
         * */
      public  void SetLocalBest(int id, double error)
        { LocalBest = id; MinError = error; }
        /* Returns the Loclal Best of this Neighbourhood
         * as an intiger indicating it's id.
         * */
        public int GetLocalBest()
        { return LocalBest; }
        /* Returns the Local Best Error as 
         * a double value.
         * */
        public double GetLocalMinError()
        {
            return MinError;
        }

    }
}
