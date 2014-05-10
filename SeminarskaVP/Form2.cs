using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeminarskaVP
{
    public partial class Form2 : Form
    {
        public int VremePogodok { get; set; }
        public bool NewGame { get; set; }
        public bool CloseApp { get; set; }
        public bool VneseniPodatoci { get; set; }
        private List<Igrac> siteIgraci;
        public static int MATRICA6X6 = 3;
        public int matricaOdbrana { get; set; }

        public Form2(int pom, bool newGame, bool closeA, int mat)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            VremePogodok = pom;
            NewGame = newGame;
            CloseApp = closeA;
            VneseniPodatoci = false;
            textBox1.Select();
            matricaOdbrana = mat;
            if (matricaOdbrana != MATRICA6X6)
            {
                System.IO.StreamReader file = new System.IO.StreamReader(Application.StartupPath.ToString() + @"\MemoryData\rezultati.txt");
                string line = file.ReadLine();
                siteIgraci = new List<Igrac>();
                while (line != null)
                {
                    string[] parts = line.Split(' ');
                    Igrac i = new Igrac(parts[0], parts[1]);
                    siteIgraci.Add(i);
                    line = file.ReadLine();
                }
                siteIgraci.Sort(new MyComparator());
                lb1.Items.Clear();
                int brojac = 0;
                int j = 0;
                while (j < siteIgraci.Count)
                {
                    Igrac igr = (Igrac)siteIgraci[j];
                    Pecati pecati = new Pecati(igr.ImeIgrac, igr.Vreme, j + 1);
                    lb1.Items.Add(pecati);
                    j++;
                    brojac++;
                    if (brojac == 10)
                    {
                        break;
                    }
                }
                file.Close();
                int min = VremePogodok / 60;
                int sec = VremePogodok % 60;

                textBox2.Text = string.Format("{0:00}:{1:00}", min, sec);
            }

            if (matricaOdbrana == MATRICA6X6)
            {
                System.IO.StreamReader file = new System.IO.StreamReader(Application.StartupPath.ToString() + @"\MemoryData\rezultati6.txt");
                string line = file.ReadLine();
                siteIgraci = new List<Igrac>();
                while (line != null)
                {
                    string[] parts = line.Split(' ');
                    Igrac i = new Igrac(parts[0], parts[1]);
                    siteIgraci.Add(i);
                    line = file.ReadLine();
                }
                siteIgraci.Sort(new MyComparator());
                lb1.Items.Clear();
                int brojac = 0;
                int j = 0;
                while (j < siteIgraci.Count)
                {
                    Igrac igr = (Igrac)siteIgraci[j];
                    Pecati pecati = new Pecati(igr.ImeIgrac, igr.Vreme, j + 1);
                    lb1.Items.Add(pecati);
                    j++;
                    brojac++;
                    if (brojac == 10)
                    {
                        break;
                    }
                }
                file.Close();
                int min = VremePogodok / 60;
                int sec = VremePogodok % 60;

                textBox2.Text = string.Format("{0:00}:{1:00}", min, sec);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int min = VremePogodok / 60;
            int sec = VremePogodok % 60;
            string vreme = string.Format("{0:00}:{1:00}", min, sec);

            string imeIgrac = textBox1.Text.Trim();
            Igrac i = new Igrac(imeIgrac, vreme);
            siteIgraci.Add(i);

            if (matricaOdbrana != MATRICA6X6)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(Application.StartupPath.ToString() + @"\MemoryData\rezultati.txt");
                for (int j = 0; j < siteIgraci.Count; j++)
                {
                    Igrac igrac = (Igrac)siteIgraci[j];
                    StringBuilder sb = new StringBuilder();
                    sb.Append(igrac.ImeIgrac);
                    sb.Append(" ");
                    sb.Append(igrac.Vreme);
                    file.WriteLine(sb.ToString());
                }
                file.Close();
            }

            if (matricaOdbrana == MATRICA6X6)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(Application.StartupPath.ToString() + @"\MemoryData\rezultati6.txt");
                for (int j = 0; j < siteIgraci.Count; j++)
                {
                    Igrac igrac = (Igrac)siteIgraci[j];
                    StringBuilder sb = new StringBuilder();
                    sb.Append(igrac.ImeIgrac);
                    sb.Append(" ");
                    sb.Append(igrac.Vreme);
                    file.WriteLine(sb.ToString());
                }
                file.Close();
            }

                if (NewGame && VneseniPodatoci)
                {
                    this.Close();
                }
                if(VneseniPodatoci)
                {
                    this.Close();
                }

        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (textBox1.Text.Trim().Length <= 0)
            {
                errorProvider1.SetError(textBox1, "Морате да внесете име!!!");
                textBox1.Select(0, textBox1.Text.Length);
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(textBox1, "");
                VneseniPodatoci = true;
            }

        }
        
    }
}
