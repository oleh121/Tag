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
    public partial class Game_Form : Form
    {
        Game game;
        User u;
        bool on = false;
        int startValue = 0;
        int clik = 0;
        int temp = 0;
        public Game_Form(User u1)
        {
            InitializeComponent();
            Location_Update();
            game = new Game(4);
            User.Text = u1.name;
            u = new User(u1);
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
        
        private void Button0_Click(object sender, EventArgs e)
        {

            numb.Text = clik.ToString();
            int position = Convert.ToInt16(((Button)sender).Tag);

            game.Change(position, ref clik);
            game.Shift(position);
            refresh();
            if (game.Check_Numbers())
            {
                on = true;
                Close();

                /*MessageBox.Show("Victory");
                start_game();*/
            }
           // MessageBox.Show(position.ToString());
        }

        private Button button(int position)
        {
            switch (position)
            {
                case 0: return button0;
                case 1: return button1;
                case 2: return button2;
                case 3: return button3;
                case 4: return button4;
                case 5: return button5;
                case 6: return button6;
                case 7: return button7;
                case 8: return button8;
                case 9: return button9;
                case 10: return button10;
                case 11: return button11;
                case 12: return button12;
                case 13: return button13;
                case 14: return button14;
                case 15: return button15;
                default: return null;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            start_game();
            timer1.Start();
        }

        private void start_game()
        { 
            game.start();
            for (int j = 0; j < 100; j++)
                game.Shift_Random();
            refresh();
        }
        private void refresh()
        {
            for (int position = 0; position < 16; position++)
            {
                int nr = game.get_number(position);
                button(position).Text = game.get_number(position).ToString();
                button(position).Visible = (nr > 0);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (temp == 100)
            {
                startValue++;
                temp -= 100;
            }
            else
            {
                numb.Text = clik.ToString();
                time.Text = Int2StringTime(startValue);
                temp++;
            }
        }
        private string Int2StringTime(int time)
        {
            int hours = (time - (time % (60 * 60))) / (60 * 60);
            int minutes = (time - time % 60) / 60 - hours * 60;
            int seconds = time - hours * 60 * 60 - minutes * 60;
            return String.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            on = true;
            Close();
        }

        private void Game_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            if (on)
            {
                u.add_victory(startValue, clik);
                Victory_Form f = new Victory_Form(u);
                f.Show();
            }
            else
            {
                if (MessageBox.Show("The window will now be closed; do I really need to do this (the result of the game will not be saved)?",
                        "Exit?",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Main_Form f = new Main_Form(u);
                    f.Show();
                }
                else
                {
                    e.Cancel = true;
                    timer1.Start();
                }

            }
        }

        private void Game_Form_LocationChanged(object sender, EventArgs e)
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

        private void Game_Form_ClientSizeChanged(object sender, EventArgs e)
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