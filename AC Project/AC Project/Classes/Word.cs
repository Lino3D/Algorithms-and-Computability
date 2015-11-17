using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AC_Project.Classes
{
  public  class Word
    {

        int id { get; set; }
        int Length { get; set; }
        int[] content { get; set; }

       public Word(int _id, int _length, int[] _content)
        {
            id = _id;
            Length = _length;
            content = _content;
        }
      public int[] getWord() {
           return this.content;   }
      public int getId() {
      return this.id; }
      public int getLength()
      { 
          return this.Length; }


    }
}
