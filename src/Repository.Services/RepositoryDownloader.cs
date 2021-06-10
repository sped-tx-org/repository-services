// -----------------------------------------------------------------------
// <copyright file="RepositoryDownloader.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace Repository.Services
{
    /// <summary>
    /// Defines the <see cref="RepositoryDownloader" />.
    /// </summary>
    internal class RepositoryDownloader : IRepositoryDownloader
    {
        /// <summary>
        /// The DownloadFileAsync.
        /// </summary>
        /// <param name="address">The address<see cref="string"/>.</param>
        /// <param name="outputFile">The outputFile<see cref="string"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
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
