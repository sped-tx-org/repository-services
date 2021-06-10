using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{

    internal class RepositoryDownloader : IRepositoryDownloader
    {
        public async Task DownloadFileAsync(string address, string outputFile)
        {
            WebClient client = new WebClient();

            try
            {
                client.DownloadFile(address, outputFile);
            }
            catch (WebException ex)
            {
                Debug.Print(ex.Message);
            }

            await Task.CompletedTask;
        }
    }
}
