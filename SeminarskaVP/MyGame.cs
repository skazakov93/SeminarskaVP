using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeminarskaVP
{
    class MyGame
    {
        public Dictionary<int, List<string>> listaIme{get; set;}
        public Dictionary<int, List<string>> listaPrezime { get; set; }
        static int MAX_KLIKANA = 2;
        public int odbranIndex{get; set;}
        public Dictionary<int, string> odgovori;
        public string str;

        public MyGame(int index)
        {
            odbranIndex = index;
            listaIme = new Dictionary<int, List<string>>();
            listaPrezime = new Dictionary<int, List<string>>();
            odgovori = new Dictionary<int, string>();
            str = null;
        }

        public void DodadiIme(int broj, List<string> l)
        {   
            listaIme.Add(broj, l);
        }

        public void DodadiPrezime(int broj, List<string> l)
        {
            listaPrezime.Add(broj, l);
        }

        public void DodadiOdg(int i, string line)
        {
            odgovori.Add(i, line);
        }

        public bool BrojKlikana(int br)
        {
            if (br <= MAX_KLIKANA)
            {
                return true;
            }
            return false;
        }

        public int DaliETocno(string Ime, string Prezime)
        {
            StringBuilder sb = new StringBuilder();
            List<string> imina = new List<string>();
            List<string> prezimina = new List<string>();

            listaIme.TryGetValue(odbranIndex, out imina);
            listaPrezime.TryGetValue(odbranIndex, out prezimina);

            string novoPrezime = Prezime.ToLower();
            string novoIme = Ime.ToLower();
            novoPrezime = novoPrezime.Replace("sh", "s");
            novoPrezime = novoPrezime.Replace("ch", "c");
            novoPrezime = novoPrezime.Replace("sh", "s");
            novoPrezime = novoPrezime.Replace("kj", "k");
            novoPrezime = novoPrezime.Replace("gj", "g");
            novoPrezime = novoPrezime.Replace("dj", "d");
            novoPrezime = novoPrezime.Replace("zh", "z");
            novoPrezime = novoPrezime.Replace("lj", "l");
            novoPrezime = novoPrezime.Replace("nj", "n");

            novoIme = novoIme.Replace("sh", "s");
            novoIme = novoIme.Replace("ch", "c");
            novoIme = novoIme.Replace("sh", "s");
            novoIme = novoIme.Replace("kj", "k");
            novoIme = novoIme.Replace("gj", "g");
            novoIme = novoIme.Replace("dj", "d");
            novoIme = novoIme.Replace("zh", "z");
            novoIme = novoIme.Replace("lj", "l");
            novoIme = novoIme.Replace("nj", "n");
            novoPrezime = novoPrezime.Trim();

            //str = novoIme + " " + novoPrezime;

            HashSet<string> odgovoriIme = new HashSet<string>();
            HashSet<string> odgovoriPrezime = new HashSet<string>();

            foreach (string s in imina)
            {
                odgovoriIme.Add(s.ToLower());
            }

            for (int i = 0; i < prezimina.Count; i++)
            {
                odgovoriPrezime.Add(prezimina[i].ToLower());
            }
            int vrati = -1;
            if (((!odgovoriPrezime.Contains(Prezime.ToLower()) && !odgovoriPrezime.Contains(novoPrezime)) || Prezime == "") && ((!odgovoriIme.Contains(Ime.ToLower()) && !odgovoriIme.Contains(novoIme)) || Ime == ""))
            {
                return -1;
            }
            if (odgovoriPrezime.Contains(Prezime.ToLower()) && odgovoriIme.Contains(Ime.ToLower()) ||
                (odgovoriPrezime.Contains(novoPrezime) && odgovoriIme.Contains(novoIme)))
            {
                vrati = 1;
            }
            if (!odgovoriPrezime.Contains(Prezime.ToLower()) && !odgovoriPrezime.Contains(novoPrezime))
            {
                vrati = 0;
            }
            if (!odgovoriIme.Contains(Ime.ToLower()) && !odgovoriIme.Contains(novoIme))
            {
                vrati = 2;
            }
            return vrati;

        }
    }
}
