using System;
using System.IO;

namespace ThirdTaks
{
    public class LineCouting
    {
        private string dirName;

        public LineCouting(string dirName) { this.dirName = dirName; }

        public void calculation()
        {
            string[] files = Directory.GetFiles(dirName);
            foreach (string filePath in files)
            {
                FileInfo fi = new FileInfo(filePath);
                if (fi.Extension.Equals(".cs"))
                {
                    int cntLines = 0;
                    StreamReader sr = fi.OpenText();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                        cntLines += !line.Equals("") ? 1 : 0;

                    Console.WriteLine("{0} have {1} code lines.", filePath, cntLines);
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            LineCouting lineCouting = new LineCouting("/home/anton/Study/C-Sharp/RationalNumbers/");
            lineCouting.calculation();
        }
    }
}
