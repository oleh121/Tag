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
    public partial class Rating_Form : Form
    {
        User u;
        public Rating_Form(User u1)
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
        private void Rating_Form_Load(object sender, EventArgs e)
        {
            dataGridView1.RowCount = u.n_victory + 1;
            dataGridView1.ColumnCount = 4;

            dataGridView1.Columns[0].HeaderText = "№";
            dataGridView1.Columns[1].HeaderText = "User name";
            dataGridView1.Columns[2].HeaderText = "Time";
            dataGridView1.Columns[3].HeaderText = "Number of click";

            for (int i = 0; i < u.n_victory; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
                dataGridView1.Rows[i].Cells[1].Value = $"{u.name}";
                dataGridView1.Rows[i].Cells[2].Value = Int2StringTime(int.Parse(u.victory[i].Split(" ")[0]));
                dataGridView1.Rows[i].Cells[3].Value = u.victory[i].Split(" ")[1];
            }


            string path = @".\Users\";
            string[] dirs = Directory.GetFiles(path);
            dataGridView2.ColumnCount = 4;

            dataGridView2.Columns[0].HeaderText = "№";
            dataGridView2.Columns[1].HeaderText = "User name";
            dataGridView2.Columns[2].HeaderText = "Time";
            dataGridView2.Columns[3].HeaderText = "Number of click";

            int v = 0;
            for (int i = 0; i < dirs.Length; i++)
            {
                User temp = new User(u);
                temp.read_file(dirs[i]);

                dataGridView2.RowCount += temp.n_victory;

                if (temp.n_victory > 0)
                {
                    for (int j = 0; j < temp.n_victory; j++)
                    {
                        dataGridView2.Rows[v].Cells[0].Value = v + 1;
                        dataGridView2.Rows[v].Cells[1].Value = $"{temp.name}";
                        dataGridView2.Rows[v].Cells[2].Value = Int2StringTime(int.Parse(temp.victory[j].Split(" ")[0]));
                        dataGridView2.Rows[v].Cells[3].Value = temp.victory[j].Split(" ")[1];
                        v++;
                    }
                }
            }

            v = 0;
            dataGridView3.ColumnCount = 4;

            dataGridView3.Columns[0].HeaderText = "№";
            dataGridView3.Columns[1].HeaderText = "User name";
            dataGridView3.Columns[2].HeaderText = "Time";
            dataGridView3.Columns[3].HeaderText = "Number of click";

            for (int i = 0; i < dirs.Length; i++)
            {
                User temp = new User(u);
                temp.read_file(dirs[i]);


                string[] temp1 = new string[3];

                

                if (temp.n_victory > 0)
                {
                    dataGridView3.RowCount += 1;
                    temp1[0] = $"{temp.name}";
                    temp1[1] = temp.victory[0].Split(" ")[0];
                    temp1[2] = temp.victory[0].Split(" ")[1];

                    for (int j = 0; j < temp.n_victory; j++)
                    {
                        if (int.Parse(temp1[1]) > int.Parse(temp.victory[j].Split(" ")[0])) {
                            temp1[0] = $"{temp.name}";
                            temp1[1] = temp.victory[j].Split(" ")[0];
                            temp1[2] = temp.victory[j].Split(" ")[1];
                        }
                    }
                    dataGridView3.Rows[v].Cells[0].Value = v + 1;
                    dataGridView3.Rows[v].Cells[1].Value = temp1[0];
                    dataGridView3.Rows[v].Cells[2].Value = Int2StringTime(int.Parse(temp1[1])); 
                    dataGridView3.Rows[v].Cells[3].Value = temp1[2];
                    v++;
                }
            }
        }

        private string Int2StringTime(int time)
        {
            int hours = (time - (time % (60 * 60))) / (60 * 60);
            int minutes = (time - time % 60) / 60 - hours * 60;
            int seconds = time - hours * 60 * 60 - minutes * 60;
            return String.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows[0].Index>= u.n_victory) {
                MessageBox.Show("You are trying to delete a non-existent item...",
                        "ERROR",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
            }
            u.victory.RemoveAt(dataGridView1.SelectedRows[0].Index);
            u.n_victory--;

            dataGridView1.RowCount = u.n_victory + 1;
            dataGridView1.ColumnCount = 4;
            for (int i = 0; i < u.n_victory; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
                dataGridView1.Rows[i].Cells[1].Value = $"{u.name}";
                dataGridView1.Rows[i].Cells[2].Value = Int2StringTime(int.Parse(u.victory[i].Split(" ")[0]));
                dataGridView1.Rows[i].Cells[3].Value = u.victory[i].Split(" ")[1];
            }

        }

        private void Rating_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Main_Form f = new Main_Form(u);
            f.Show();
        }

        private void Rating_Form_LocationChanged(object sender, EventArgs e)
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

        private void Rating_Form_ClientSizeChanged(object sender, EventArgs e)
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
