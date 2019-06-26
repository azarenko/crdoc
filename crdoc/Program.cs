using System;
using System.IO;
using System.Text.RegularExpressions;

namespace crdoc
{
    class Program
    {
        static void Main(string[] args)
        {
            Scan(args[0], args[1]);
        }

        static Regex re = new Regex("^(.*)\\/\\/\\/\\<cr\\>([^\\<]+)\\<\\/cr\\>$");

        static void Scan(string path, string filter)
        {
            if (!Directory.Exists(path))
                return;

            foreach (string filename in Directory.GetFiles(path, filter))
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    for(int i=0; !sr.EndOfStream ;i++)
                    {
                        string line = sr.ReadLine();
                        Match m = re.Match(line);

                        if (m.Success)
                        {
                            Console.WriteLine(string.Format("{0}\t{1}\t{2}", m.Groups[2].Value, m.Groups[1].Value, i));
                        }
                    }
                }
            }

            foreach(string directorypath in Directory.GetDirectories(path))
            {
                Scan(directorypath, filter);
            }
        }
    }
}
