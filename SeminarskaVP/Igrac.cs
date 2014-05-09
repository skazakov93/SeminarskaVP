using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeminarskaVP
{
    class Igrac
    {
        public string ImeIgrac { get; set; }
        public string Vreme { get; set; }

        public Igrac(string i, string v)
        {
            ImeIgrac = i;
            Vreme = v;

        }

        public override string ToString()
        {
            return string.Format("{0} {1:00}", ImeIgrac, Vreme);
        }


    }
}
