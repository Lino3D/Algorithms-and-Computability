using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Project.Classes
{
    /* The class that handles a single word.
     * It consists of a id - integer value
     * Length - integer value
     * and the content - as an array of integers
     * */
  public  class Word
    {

        int id { get; set; }
        int Length { get; set; }
        int[] content { get; set; }
      /* Constructs word having given the id, length and the content */
       public Word(int _id, int _length, int[] _content)
        {
            id = _id;
            Length = _length;
            content = _content;
        }
      /* return the word's content - as an array
       * of integer values
       * */
      public int[] getWord() {
           return this.content;   }
      /* Returns the id of the word
       * as an integer value.
       * */
      public int getId() {
      return this.id; }
      /* Returns the Length of the word
       * as the integer value.
       * */
      public int getLength()
      { 
          return this.Length; }


    }
}
