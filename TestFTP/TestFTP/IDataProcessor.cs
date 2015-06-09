using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TestWCFProxy;

namespace TestFTP
{
    public interface IDataProcessor
    {
        bool ProcessData(string upload);
    }

    public class DataProcessor : IDataProcessor
    {
        private readonly string _baseAddress;
        private readonly string _url;

        public DataProcessor(string baseAddress, string url)
        {
            _baseAddress = baseAddress;
            _url = url;
        }

        public bool ProcessData(string upload)
        {
            var rows = upload.Split('\n').Select(row => row.Split(','));

            var toSumbit = new List<ClientData>();

            foreach (var row in rows)
            {
                if (IsIgnorableBlankRow(row)) 
                    continue;

                if (row.Count() != 5)
                    return false;

                toSumbit.Add(new ClientData(row[0], row[1], row[2], row[3], row[4]));
            }

            SendDataToService(toSumbit);

            return true;
        }

        private void SendDataToService(List<ClientData> toSumbit)
        {
            SubmitData(toSumbit, _baseAddress, _url).Wait();
        }

        private static async Task SubmitData(List<ClientData> toSubmit, string baseAddress, string _url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync(_url, toSubmit);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Sent {0} records successfully", toSubmit.Count);
                }
                else
                {
                    Console.WriteLine("ERROR!! {0} - {1}", response.StatusCode, response.ReasonPhrase);
                }
            }
        }

        private static bool IsIgnorableBlankRow(string[] row)
        {
            return row.Count() == 1 && row[0].Trim().Length == 0;
        }
    }
}