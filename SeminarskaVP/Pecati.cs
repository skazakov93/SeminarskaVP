using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeminarskaVP
{
    class Pecati : Igrac
    {
        public int Pozicija { get; set; }

        public Pecati(string ime, string v, int poz) : base(ime, v)
        {
            Pozicija = poz;
        }

        public override string ToString()
        {
            return Pozicija + ". " + this.ImeIgrac + " " + this.Vreme;
        }
    }
}
