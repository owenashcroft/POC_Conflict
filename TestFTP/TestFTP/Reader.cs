using System.IO;

namespace TestFTP
{
    public class Reader : IReader
    {
        private readonly StreamReader _reader;

        public Reader(StreamReader reader)
        {
            _reader = reader;
        }

        public string Read()
        {
            return _reader.ReadLine();
        }
    }
}