using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SeminarskaVP
{
    public partial class Form5 : Form
    {
        private List<Igrac> siteIgraci;

        public Form5()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            string path = Application.StartupPath.ToString();
            string[] parts1 = path.Split('\\');
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < parts1.Length - 2; i++)
            {
                sb.Append(parts1[i]);
                sb.Append("\\");
            }

            System.IO.StreamReader file = new System.IO.StreamReader(sb.ToString() + @"\Properties\MemoryData\rezultati.txt");
            string line = file.ReadLine();
            siteIgraci = new List<Igrac>();
            while (line != null)
            {
                string[] parts = line.Split(' ');
                if (!(parts.Count() < 2))
                {
                    Igrac i = new Igrac(parts[0], parts[1]);
                    siteIgraci.Add(i);
                }
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

            path = Application.StartupPath.ToString();
            parts1 = path.Split('\\');
            sb = new StringBuilder();
            for (int i = 0; i < parts1.Length - 2; i++)
            {
                sb.Append(parts1[i]);
                sb.Append("\\");
            }

            file = new System.IO.StreamReader(sb.ToString() + @"\Properties\MemoryData\rezultati6.txt");
            line = file.ReadLine();
            siteIgraci = new List<Igrac>();
            while (line != null)
            {
                string[] parts = line.Split(' ');
                if(!(parts.Count() < 2)){
                Igrac i = new Igrac(parts[0], parts[1]);
                siteIgraci.Add(i);
                }
                line = file.ReadLine();
            }
            siteIgraci.Sort(new MyComparator());
            lb2.Items.Clear();
            brojac = 0;
            j = 0;
            while (j < siteIgraci.Count)
            {
                Igrac igr = (Igrac)siteIgraci[j];
                Pecati pecati = new Pecati(igr.ImeIgrac, igr.Vreme, j + 1);
                lb2.Items.Add(pecati);
                j++;
                brojac++;
                if (brojac == 10)
                {
                    break;
                }
            }
            file.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path1 = Application.StartupPath.ToString();
            string[] parts1 = path1.Split('\\');
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < parts1.Length - 2; i++)
            {
                sb.Append(parts1[i]);
                sb.Append("\\");
            }

            String path = sb.ToString() + @"\Properties\MemoryData\rezultati.txt";
            using (var stream = new FileStream(path, FileMode.Truncate))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write("");
                }
            }
            lb1.Items.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path1 = Application.StartupPath.ToString();
            string[] parts1 = path1.Split('\\');
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < parts1.Length - 2; i++)
            {
                sb.Append(parts1[i]);
                sb.Append("\\");
            }

            String path = sb.ToString() + @"\Properties\MemoryData\rezultati6.txt";
            using (var stream = new FileStream(path, FileMode.Truncate))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write("");
                }
            }
            lb2.Items.Clear();
        }   
    }
}
