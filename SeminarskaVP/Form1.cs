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
    
    public partial class Form1 : Form
    {
        private Label prvKlik;
        private Label vtorKlik;
        private int Vreme;
        private int min;
        private int sec;
        private int PreostanatoVreme;
        static int BROJ_NA_SLIKI = 10;
        private MyGame game;
        private bool Klikaj;
        private HashSet<string> tocniOdg;
        private int vremePogodok;
        //public bool q = false;
        public bool nivoTezina;
        public List<string> lista;
        int h , pomosCount;
        public bool dali;
        public bool flagH;
        public int mat;
        public bool golemina;
        public int promasuvanja;
        private Random ran;

        public Form1()
        {
            InitializeComponent();
            nivoTezina = true;
            //tbp2.Visible = true;
            //pictureBox2.Visible = true;
            //tablePanel.Visible = false;
            //pictureBox1.Visible = false;
            ran = new Random();
            NewGame();
            this.DoubleBuffered = true;
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
        private void tablePanel_MouseClick(object sender, MouseEventArgs e)
        {
            //tablePanel.Visible = false;
        }
       
        

        private bool Pogodi(int vreme, string ime, string prezime)
        {
            if (vreme == -1 && nivoTezina)
            {
                timer2.Stop();                
                tbp2.Visible = false;
                DialogResult rez = MessageBox.Show("Времето ви истече, не успеавте да ја погодите сложувалката !!!!\nСакате да започнете нова игра", "", MessageBoxButtons.YesNo);
                if (rez == DialogResult.No)
                {
                    Application.Exit();
                }
                if (rez == DialogResult.Yes)
                {
                    NewGame();
                }
                return false;
            }
            return true;
        }

        private void NewGame()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            //dali = false;
            int slikaBr = ran.Next(BROJ_NA_SLIKI) + 1;
            game = new MyGame(slikaBr);

            if (dali)
            {
                if (!golemina)
                {
                    this.Height += 35;
                    golemina = true;
                    //button3.Location = new Point(Width, Height+20);
                    button3.Top = 335;
                }
                tbp2.Visible = true;
                pictureBox2.Visible = true;
                tablePanel.Visible = false;
                pictureBox1.Visible = false;
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                pomosCount = 0;
                flagH = false;
                Vreme = 180;
                promasuvanja = 0;
                toolStripStatusLabel1.Text = "Неуспешни обиди : " + promasuvanja;
                Klikaj = true;
                //brojacKlikana = 0;
                timer2.Start();
                PreostanatoVreme = Vreme;
                vremePogodok = 0;
                min = Vreme / 60;
                sec = Vreme % 60;
                label4.Text = string.Format("{0:00}:{1:00}", min, sec);
                textBox1.Text = "";
                textBox2.Text = "";
                lista = new List<string>() { 
                    "!", "N", ",", "k",
                    "b", "v", "w", "z", "1", "2",
                    "3", "Z", "5", "^", "7", "P",
                    "T", "H"
                };
                string path = Application.StartupPath.ToString();
                string[] parts1 = path.Split('\\');
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < parts1.Length - 2; i++)
                {
                    sb.Append(parts1[i]);
                    sb.Append("\\");
                }

                System.IO.StreamReader file = new System.IO.StreamReader(sb.ToString().ToString() + @"\Properties\MemoryData\odgovori.txt");
               
                for (int i = 0; i < BROJ_NA_SLIKI; i++)
                {
                    string line = file.ReadLine();
                    string[] parts = line.Split(' ');

                    tocniOdg = new HashSet<string>();
                    List<string> niza = new List<string>();
                    foreach (string s in parts)
                    {
                        tocniOdg.Add(s.ToLower());
                        niza.Add(s);
                    }
                    game.DodadiIme(i + 1, niza);
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

                System.IO.StreamReader file2 = new System.IO.StreamReader(sb.ToString() + @"\Properties\MemoryData\odgovoriP.txt");

                for (int i = 0; i < BROJ_NA_SLIKI; i++)
                {
                    string line = file2.ReadLine();
                    string[] parts = line.Split(' ');

                    tocniOdg = new HashSet<string>();
                    List<string> niza = new List<string>();
                    foreach (string s in parts)
                    {
                        tocniOdg.Add(s.ToLower());
                        niza.Add(s);
                    }
                    game.DodadiPrezime(i + 1, niza);
                }
                file2.Close();

                
                tbp2.SuspendLayout();

                string pateka = "";
                pateka = @"Properties\MemoryData\" + slikaBr + ".jpg";

                path = Application.StartupPath.ToString();
                parts1 = path.Split('\\');
                sb = new StringBuilder();
                for (int i = 0; i < parts1.Length - 2; i++)
                {
                    sb.Append(parts1[i]);
                    sb.Append("\\");
                }

                Image image = Image.FromFile(sb.ToString() + pateka);

                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox2.Image = image;
                pictureBox2.Visible = false;
                Bitmap bmp = ScaleImage(image, tbp2.Width + 4, tbp2.Height + 4);

                //tbp2.BackgroundImage = bmp;
                tbp2.Visible = true;
                List<string> brojki = new List<string>() { 
                    "!", "!", "N", "N", ",", ",", "k", "k",
                    "b", "b", "v", "v", "w", "w", "z", "z", "1", "1", "2", "2",
                    "3", "3", "Z", "Z", "5", "5", "^", "^", "7", "7", "P", "P",
                    "T", "T", "H", "H"
                };

                foreach (Control c in tbp2.Controls)
                {
                    Label pom = c as Label;
                    if (pom != null)
                    {
                        //pom.Visible = true;
                        pom.BackColor = Color.CornflowerBlue;
                        pom.ForeColor = Color.CornflowerBlue;
                        //pom.Enabled = true;   
                    }
                    pom.Enabled = true;
                }
                prvKlik = null;
                vtorKlik = null;

                foreach (Control c in tbp2.Controls)
                {
                    Label pom = c as Label;
                    if (pom != null)
                    {
                        int broj = ran.Next(brojki.Count);
                        pom.Text = brojki[broj];
                        brojki.RemoveAt(broj);
                    }
                }

                tbp2.ResumeLayout();
                pictureBox2.Visible = true;
                
                tbp2.BackgroundImage = bmp;
            }
            else {
                if (golemina)
                {
                    this.Height -= 35;
                    golemina = false;
                    //button3.Location = new Point(Width, Height+20);
                    button3.Top = 300;
                }
                tablePanel.Visible = true;
                pictureBox1.Visible = true;
                tbp2.Visible = false;
                pictureBox2.Visible = false;
                Vreme = 90;
                promasuvanja = 0;
                toolStripStatusLabel1.Text = "Неуспешни обиди : " + promasuvanja;
                Klikaj = true;
                flagH = false;
                timer2.Start();
                PreostanatoVreme = Vreme;
                vremePogodok = 0;
                min = Vreme / 60;
                sec = Vreme % 60;
                label4.Text = string.Format("{0:00}:{1:00}", min, sec);
                textBox1.Text = "";
                textBox2.Text = "";
                pomosCount = 0;
                nivoTezina = true;
                lista = new List<string>() { 
                    "!", "N", ",", "k", "b", "v", "w", "z"
                };

                string path2 = Application.StartupPath.ToString();
                string[] parts1 = path2.Split('\\');
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < parts1.Length - 2; i++)
                {
                    sb.Append(parts1[i]);
                    sb.Append("\\");
                }

                System.IO.StreamReader file = new System.IO.StreamReader(sb.ToString() + @"\Properties\MemoryData\odgovori.txt");

                for (int i = 0; i < BROJ_NA_SLIKI; i++)
                {
                    string line = file.ReadLine();
                    string[] parts = line.Split(' ');

                    tocniOdg = new HashSet<string>();
                    List<string> niza = new List<string>();
                    foreach (string s in parts)
                    {
                        tocniOdg.Add(s.ToLower());
                        niza.Add(s);
                    }
                    game.DodadiIme(i + 1, niza);
                }

                file.Close();

                string path = Application.StartupPath.ToString();
                string[] parts2 = path.Split('\\');
                sb = new StringBuilder();
                for (int i = 0; i < parts1.Length - 2; i++)
                {
                    sb.Append(parts2[i]);
                    sb.Append("\\");
                }

                System.IO.StreamReader file2 = new System.IO.StreamReader(sb.ToString() + @"\Properties\MemoryData\odgovoriP.txt");

                for (int i = 0; i < BROJ_NA_SLIKI; i++)
                {
                    string line = file2.ReadLine();
                    string[] parts = line.Split(' ');

                    tocniOdg = new HashSet<string>();
                    List<string> niza = new List<string>();
                    foreach (string s in parts)
                    {
                        tocniOdg.Add(s.ToLower());
                        niza.Add(s);
                    }
                    game.DodadiPrezime(i + 1, niza);
                }
                file2.Close();

                tablePanel.SuspendLayout();
                string pateka = "";
                pateka = @"Properties\MemoryData\" + slikaBr + ".jpg";

                path = Application.StartupPath.ToString();
                parts1 = path.Split('\\');
                sb = new StringBuilder();
                for (int i = 0; i < parts1.Length - 2; i++)
                {
                    sb.Append(parts1[i]);
                    sb.Append("\\");
                }

                Image image = Image.FromFile(sb.ToString() + pateka);
                pictureBox1.Image = image;

                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Visible = false;
                //tablePanel.BackgroundImage = pictureBox1.Image;
                tablePanel.Visible = true;
                List<string> brojki = new List<string>() { 
                    "!", "!", "N", "N", ",", ",", "k", "k",
                    "b", "b", "v", "v", "w", "w", "z", "z"
                };

                foreach (Control c in tablePanel.Controls)
                {
                    Label pom = c as Label;
                    if (pom != null)
                    {
                        //pom.Visible = true;
                        pom.BackColor = Color.CornflowerBlue;
                        pom.ForeColor = Color.CornflowerBlue;
                        //pom.Enabled = true;
                    }
                    pom.Enabled = true;
                }

                prvKlik = null;
                vtorKlik = null;
                textBox1.Select();

                foreach (Control c in tablePanel.Controls)
                {
                    Label pom = c as Label;
                    if (pom != null)
                    {
                        int broj = ran.Next(brojki.Count);
                        pom.Text = brojki[broj];
                        brojki.RemoveAt(broj);
                    }
                }
                tablePanel.ResumeLayout();
                pictureBox1.Visible = true;
                tablePanel.BackgroundImage = pictureBox1.Image;
            }
        }
        static public Bitmap ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            double X = (double)maxWidth / image.Width;
            double Y = (double)maxHeight / image.Height;
            double razmer = Math.Min(X, Y);

            int newWidth = (int)(image.Width * razmer);
            int newHeight = (int)(image.Height * razmer);

            Bitmap newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
            Bitmap bmp = new Bitmap(newImage);

            return bmp;
        }

        private void Klik(object sender, EventArgs e)
        {
            //this.SuspendLayout();
            if (dali) {
                tbp2.SuspendLayout();
            }
            else
                tablePanel.SuspendLayout();
            Label kliknato = sender as Label;
            if (kliknato != null && Klikaj)
            {
                if (kliknato.ForeColor == Color.Black)
                {
                    return;
                }
                if (prvKlik == null)
                {
                    prvKlik = kliknato;
                    prvKlik.ForeColor = Color.Black;
                    return;
                }
                vtorKlik = kliknato;
                vtorKlik.ForeColor = Color.Black;
                
                if (prvKlik.Text == vtorKlik.Text)
                {
                    lista.Remove(prvKlik.Text);                   
                    //Klikaj = false;
                    timer3.Start();
                    return;
                }
                if (prvKlik != null && vtorKlik != null)
                {
                    promasuvanja++;
                    toolStripStatusLabel1.Text = "Неуспешни обиди : " + promasuvanja;
                    Klikaj = false;
                }
                timer1.Start();
            }
            if (dali)
            {
                tbp2.ResumeLayout();
            }
            else
                tablePanel.ResumeLayout();
        }
        
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dali)
            {
                tbp2.SuspendLayout();
            }
            else
                tablePanel.SuspendLayout();
            Klikaj = true;
            timer1.Stop();
            
            prvKlik.ForeColor = prvKlik.BackColor;
            vtorKlik.ForeColor = vtorKlik.BackColor;
            prvKlik = null;
            vtorKlik = null;
            if (dali)
            {
                tbp2.ResumeLayout();
            }
            else
                tablePanel.ResumeLayout();
            
        }

        

        private void timer2_Tick(object sender, EventArgs e)
        {
            //timer2.Start();
            //timer2.Stop();
            Vreme = Vreme - timer2.Interval / 1000;
            vremePogodok++;
            //label18.Text = Vreme + "";
            if (Pogodi(Vreme, textBox1.Text, textBox2.Text))
            {
                min = Vreme / 60;
                sec = Vreme % 60;
                label4.Text = string.Format("{0:00}:{1:00}", min, sec);
            }
        }

        public void isCorrect(string s1, string s2)
        {
            if (dali)
            {
                tbp2.SuspendLayout();
            }
            else
                tablePanel.SuspendLayout();
            if (game.DaliETocno(s1, s2) == -1)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Select();
            }
                if (game.DaliETocno(s1, s2) == 1)
                {
                    if (dali)
                    {
                        tbp2.Visible = false;
                        mat = 3;
                    }
                    else
                    {
                        tablePanel.Visible = false;
                        mat = 0;
                    }
                    timer2.Stop();
                    Form3 f3 = new Form3(vremePogodok +pomosCount, Width, Height, mat);
                    f3.ShowDialog();
                    if (f3.CloseApp)
                    {
                        this.Close();
                    }
                    if (f3.NovaIgra)
                    {                      
                        NewGame();
                    }
                }

            if (game.DaliETocno(s1, s2) == 0)
            {
                textBox2.Text = "";
                textBox2.Select();
            }
            if (game.DaliETocno(s1, s2) == 2)
            {
                textBox1.Text = "";
                textBox1.Select();
            }
            //label18.Text = game.DaliETocno(pom);
            if (dali)
            {
                tbp2.ResumeLayout();
            }
            else
                tablePanel.ResumeLayout();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isCorrect(textBox1.Text.Trim(), textBox2.Text.Trim());
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (dali)
            {
                tbp2.SuspendLayout();
            }
            else
                tablePanel.SuspendLayout();
            Klikaj = true;
            timer3.Stop();
            //prvKlik.Visible = false;
            //vtorKlik.Visible = false;
            prvKlik.Text = "";
            vtorKlik.Text = "";
            prvKlik.BackColor = Color.Transparent;
            vtorKlik.BackColor = Color.Transparent;
            prvKlik.Enabled = false;
            vtorKlik.Enabled = false;
            prvKlik = null;
            vtorKlik = null;
            if (dali)
            {
                tbp2.ResumeLayout();
            }
            else
                tablePanel.ResumeLayout();
            
        }       

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //NewGame();
        }

        private void x6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*Form4 form = new Form4();
            this.Visible = false;
            timer2.Stop();
            timer3.Stop();
            timer1.Stop();
            nivoTezina = false;
            form.ShowDialog();
            this.Close();*/
            dali = true;
            //tbp2.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            NewGame();

        }

        private void x4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dali = false;
            //tablePanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            NewGame();          
        }

        private void highScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            timer2.Stop();
            f5.ShowDialog();
            timer2.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Select();
            if (dali && !flagH)
            {
                h = 4;
                flagH = true;
                
            }
            else if(!dali && !flagH)
            {
                h = 2;
                flagH = true;
                
            }
            if (h > 0 && lista.Count > 0)
            {   
                h--;
                Random r = new Random();
                int index = r.Next(lista.Count);
                if (lista.Count > 0)
                {
                    string randomString = lista[index];
                    lista.RemoveAt(index);
                    if (dali && (Vreme - 20) > 0)
                    {
                        Vreme -= 20;
                        pomosCount += 20;
                        tbp2.SuspendLayout();
                        foreach (Label i in tbp2.Controls)
                        {
                            if (i.Text == randomString)
                            {
                                i.Text = "";
                                i.BackColor = Color.Transparent;
                                i.Enabled = false;
                            }
                        }
                        tbp2.ResumeLayout();
                    }
                    if (!dali && (Vreme - 10) > 0)
                    {
                        Vreme -= 10;
                        pomosCount += 10;
                        tablePanel.SuspendLayout();
                        foreach (Label i in tablePanel.Controls)
                        {
                            if (i.Text == randomString)
                            {
                                i.Text = "";
                                i.BackColor = Color.Transparent;
                                i.Enabled = false;
                            }
                        }
                        tablePanel.ResumeLayout();
                    }
                }
                //Invalidate(true);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!dali)
            {
                tablePanel.Visible = false;
            }
            else tbp2.Visible = false;
            timer2.Stop();
            List<string> Ime;
            game.listaIme.TryGetValue(game.odbranIndex, out Ime);
            List<string> Prez;
            game.listaPrezime.TryGetValue(game.odbranIndex, out Prez);

            DialogResult rez = MessageBox.Show("Не успеавте да ја погодите сложувалката (" + Ime[0] + " " + Prez[0] + " ) !!!!\nСакате да започнете нова игра", "", MessageBoxButtons.YesNo);
            if (rez == DialogResult.No)
            {
                Application.Exit();
            }
            if (rez == DialogResult.Yes)
            {
                NewGame();
            }
            
            
        }

       
        private void lb1_Click_1(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lb2_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lb3_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lb4_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lb5_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lb6_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lb7_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lb8_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lb9_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lb10_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lb11_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lb12_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lb13_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lb14_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lb15_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lb16_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl1_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl2_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl3_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl4_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl5_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl6_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl7_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl8_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl9_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl10_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl11_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl12_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl13_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl14_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl15_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl16_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl17_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl18_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl19_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl20_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl21_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl22_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl23_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl24_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl25_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl26_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl27_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl28_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl29_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl30_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl31_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl32_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl33_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl34_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl35_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void lbl36_Click(object sender, EventArgs e)
        {
            Klik(sender, e);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

    }
}
