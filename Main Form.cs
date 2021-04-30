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
    
    public partial class Main_Form : Form
    {
        bool on = false;
        User u1 = new User("Guest");
        public Main_Form()
        {
            InitializeComponent();
            label2.Text = u1.name;
            on = false;

            // Create a file to write to.
            string path = @".\Resorce\login.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(this.Location.X);
                    sw.WriteLine(this.Location.Y);
                    sw.WriteLine(this.Size.Width);
                    sw.WriteLine(this.Size.Height);
                }
            }
        }
        public Main_Form(User u)
        {
            InitializeComponent();
            Location_Update();
            u1 = new User(u);
            label2.Text = u1.name;
            on = false;
        }
        private void Location_Update()
        {
            // Open the file to read from.
            string path = @".\Resorce\login.txt"; int[] log = new int[4];
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
        private void Start_Click(object sender, EventArgs e)
        {
            on = true;
            Game_Form f = new Game_Form(u1);
            f.Show();
            Close();
        }

        private void Rating_Click(object sender, EventArgs e)
        {
            on = true;
            Rating_Form f = new Rating_Form(u1);
            f.Show();
            Close();
        }

        private void Users_Click(object sender, EventArgs e)
        {
            on = true;
            Users_Form f = new Users_Form(u1);
            f.Show();
            Close();

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (on)
            {
                this.Hide();
            }
            else
            {
                Application.Exit();
                File.Delete(@".\Users\Guest.txt");
                if (MessageBox.Show("The game will now be closed; do you really need to do this?",
                        "Exit?",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    
                }
                else
                    e.Cancel = true;
            }
        }

        private void Main_Form_LocationChanged(object sender, EventArgs e)
        {
            File.Delete(@".\Resorce\login.txt");
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

        private void Main_Form_ClientSizeChanged(object sender, EventArgs e)
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
