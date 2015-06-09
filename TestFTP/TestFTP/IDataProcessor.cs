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
        private readonly ITestProject _testProjectService;
        private readonly int _recordsPerSubmission;

        public DataProcessor(ITestProject testProjectService, int recordsPerSubmission)
        {
            _testProjectService = testProjectService;
            _recordsPerSubmission = recordsPerSubmission;
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
            SubmitData(toSumbit).Wait();
        }

        private static async Task SubmitData(List<ClientData> toSubmit)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.PostAsJsonAsync("webapitest/api/main/submit", toSubmit);

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