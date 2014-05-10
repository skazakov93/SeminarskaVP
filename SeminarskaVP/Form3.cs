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
    public partial class Form3 : Form
    {
        public bool NovaIgra { get; set; }
        public bool CloseApp { get; set; }
        public int VremePogodok { get; set; }
        public static int MATRICA6X6 = 3;
        public int kojaMatrica { get; set; }

        public Form3(int vreme, int sirina, int visina, int mat)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            checkBox1.Visible = false;
            checkBox1.Checked = true;
            NovaIgra = true;
            CloseApp = false;
            VremePogodok = vreme;
            int monitorSirina = Screen.PrimaryScreen.Bounds.Width;
            int monitorVisina = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(monitorSirina - sirina, monitorVisina - visina);
            kojaMatrica = mat;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(VremePogodok, NovaIgra, CloseApp, kojaMatrica);
            f2.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NovaIgra = true;
            CloseApp = false;
            this.Close();
        }
    }
   
}
