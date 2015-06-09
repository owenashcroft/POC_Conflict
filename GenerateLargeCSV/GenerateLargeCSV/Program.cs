using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateLargeCSV
{
    class Program
    {
        private static readonly string _fileName = @"c:\temp\generated.csv";

        static void Main(string[] args)
        {
            Console.WriteLine("Columns");
            var columns = int.Parse(Console.ReadLine());
            Console.WriteLine("Size of file (MB)");
            var totalSize = long.Parse(Console.ReadLine());

            if (File.Exists(_fileName))
                File.Delete(_fileName);



            using(var fs = new FileStream(_fileName, FileMode.CreateNew))
            using (var writer = new StreamWriter(fs, Encoding.ASCII))
            {
                while (GetFileSizeInMegaBytes() < totalSize)
                {
                     Console.SetCursorPosition(0, Console.CursorTop);
                     Console.Write("Size {0:##,###,##0.00} / {1:##,###,##0.00}", GetFileSizeInMegaBytes(), totalSize);
                    

                    var text = new List<string>();
                    for (var i = 0; i < columns; i++)
                    {
                        text.Add(Guid.NewGuid().ToString());
                    }

                    writer.Write(string.Join(",", text) + "\n");
                    writer.Flush();
                }
            }

        }

        private static double GetFileSizeInMegaBytes()
        {
            return new FileInfo(_fileName).Length * 1e-6;
        }
    }
}
