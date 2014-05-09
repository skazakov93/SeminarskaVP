using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeminarskaVP
{
    class MyComparator : Comparer<Igrac>
    {
        public override int Compare(Igrac x, Igrac y)
        {
            return x.Vreme.CompareTo(y.Vreme);
            //throw new NotImplementedException();
        }
    }
}
