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

namespace Game
{
    public partial class Victory_Form : Form
    {
        User u;
        public Victory_Form(User u1)
        {
            InitializeComponent();
            Location_Update();

            u = new User(u1);
            label2.Text = u1.name;
            label4.Text = Int2StringTime(int.Parse(u1.victory[u1.victory.Count - 1].Split(" ")[0])) ;
            label6.Text = u1.victory[u1.victory.Count - 1].Split(" ")[1];
        }
        private void Location_Update()
        {
            // Open the file to read from.
            string path = @".\Resorce\login.txt";
            int[] log = new int[4];
            using (StreamReader sr = File.OpenText(path))
            {

                log[0] = int.Parse(sr.ReadLine());
                log[1] = int.Parse(sr.ReadLine());
                log[2] = int.Parse(sr.ReadLine());
                log[3] = int.Parse(sr.ReadLine());
            }
            this.StartPosition = FormStartPosition.Manual;
            this.Top = log[0];
            this.Left = log[1];
            this.Size = new Size(log[2], log[3]);

        }
        private string Int2StringTime(int time)
        {
            int hours = (time - (time % (60 * 60))) / (60 * 60);
            int minutes = (time - time % 60) / 60 - hours * 60;
            int seconds = time - hours * 60 * 60 - minutes * 60;
            return String.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }

        private void Victory_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main_Form f = new Main_Form(u);
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Victory_Form_LocationChanged(object sender, EventArgs e)
        {
            string path = @".\Resorce\login.txt";
            File.Delete(path);
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(this.Top);
                    sw.WriteLine(this.Left);
                    sw.WriteLine(this.Size.Width);
                    sw.WriteLine(this.Size.Height);
                }
            }
        }

        private void Victory_Form_ClientSizeChanged(object sender, EventArgs e)
        {
            string path = @".\Resorce\login.txt"; File.Delete(path);
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(this.Top);
                    sw.WriteLine(this.Left);
                    sw.WriteLine(this.Size.Width);
                    sw.WriteLine(this.Size.Height);
                }
            }
        }
    }
}
