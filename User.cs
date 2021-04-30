using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Game
{
    public class User
    {
        public string name { get; set; }
        public int n_victory { get; set; }
        public List<string> victory { get; set; }
        public User(int i, string name) {
            this.name = name;
            read_file();
        }
        public User(string name) {
            this.name = name;
            victory = new List<string>(n_victory);
            //string absPathContainingHrefs = System.IO.Directory.GetCurrentDirectory();Environment.CurrentDirectory
            string path =@".\Users\" + $"{name}.txt";
            //string fullPath = Path.Combine(absPathContainingHrefs, path);
            //fullPath = Path.GetFullPath(fullPath);

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(name);
                    sw.WriteLine("0");
                }
            }

 
        }
        public User(User obj)
        {
            name = obj.name;
            n_victory = obj.n_victory;
            victory = obj.victory;
        }
        public void read_file()
        {
            string path = @".\Users\" + $"{name}.txt";

            // Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                name = sr.ReadLine();
                n_victory = int.Parse(sr.ReadLine());
                victory = new List<string>(n_victory);

                for (int i = 0; i < n_victory; i++)
                {
                    victory.Add(sr.ReadLine());
                }

            }
        }
        public void read_file(string path)
        {
            // Open the file to read from.
            using (StreamReader sr = File.OpenText(path))
            {
                name = sr.ReadLine();
                n_victory = int.Parse(sr.ReadLine());
                victory = new List<string>(n_victory);

                for (int i = 0; i < n_victory; i++)
                {
                    victory.Add(sr.ReadLine());
                }

            }
        }
        public void add_victory(int time, int clik) {
            string temp = time.ToString() + " " + clik.ToString();
            n_victory++;
            victory.Add(temp);
            write_victory();
        }
        public void write_victory()
        {
            string path = @".\Users\" + $"{name}.txt";
            File.Delete(path);
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(name);
                    sw.WriteLine(n_victory);
                    for (int i = 0; i < n_victory; i++)
                    {
                        sw.WriteLine(victory[i]);
                    }
                }
            }

        }
    }
}
