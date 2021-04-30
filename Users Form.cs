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
    public partial class Users_Form : Form
    {
        User u;
        public Users_Form(User u1)
        {
            InitializeComponent();
            Location_Update();
            u = new User(u1);
            
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
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("You did not enter a username...",
                        "ERROR",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
            else {
                User u = new User(textBox1.Text);
                listBox1.Items.Clear();
                string path = @".\Users\";
                string[] dirs = Directory.GetFiles(path);

                for (int i = 0; i < dirs.Length; i++)
                {
                    listBox1.Items.Add(dirs[i].Split("\\")[2][..^4]);
                }
                listBox1.SelectedItem = u.name;
                textBox1.Text = string.Empty;
            }
        }

        private void Select_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            u = new User(1, listBox1.SelectedItem.ToString());

            label3.Text = u.n_victory.ToString();

            if (u.n_victory <= 0)
            {
                label5.Text = "No data";
                label8.Text = "No data";
            }
            else
            {
                string[] temp1 = new string[2];
                if (u.n_victory > 0)
                {
                    temp1[0] = u.victory[0].Split(" ")[0];
                    temp1[1] = u.victory[0].Split(" ")[1];

                    for (int j = 0; j < u.n_victory; j++)
                    {
                        if (int.Parse(temp1[0]) > int.Parse(u.victory[j].Split(" ")[0]))
                        {
                            temp1[0] = u.victory[j].Split(" ")[0];
                        }
                        if (int.Parse(temp1[1]) > int.Parse(u.victory[j].Split(" ")[1]))
                        {
                            temp1[1] = u.victory[j].Split(" ")[1];
                        }
                    }
                }

                label5.Text = Int2StringTime(int.Parse(temp1[0]));
                label8.Text = temp1[1];
            }
        }
        private string Int2StringTime(int time)
        {
            int hours = (time - (time % (60 * 60))) / (60 * 60);
            int minutes = (time - time % 60) / 60 - hours * 60;
            int seconds = time - hours * 60 * 60 - minutes * 60;
            return String.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
        private void Users_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main_Form f = new Main_Form(u);
            f.Show();

        }

        private void Users_Form_Load(object sender, EventArgs e)
        {
            string path = @".\Users\";
            string[] dirs = Directory.GetFiles(path);
            
            for (int i = 0; i < dirs.Length; i++)
            {
                listBox1.Items.Add(dirs[i].Split("\\")[2][..^4]);
            }
            listBox1.SelectedItem = u.name;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count <= 1)
            {
                MessageBox.Show("You cannot remove the last user from the list!",
                        "Exit?",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
            else
            {
                string path = @".\Users\" + $"{listBox1.SelectedItem.ToString()}.txt";
                File.Delete(path);
                listBox1.Items.Clear();

                path = @".\Users\";
                string[] dirs = Directory.GetFiles(path);

                for (int i = 0; i < dirs.Length; i++)
                {
                    listBox1.Items.Add(dirs[i].Split("\\")[2][..^4]);
                }

                u = new User(1, listBox1.Items[0].ToString());
                listBox1.SelectedItem = u.name;
            }
        }

        private void Users_Form_LocationChanged(object sender, EventArgs e)
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

        private void Users_Form_ClientSizeChanged(object sender, EventArgs e)
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
