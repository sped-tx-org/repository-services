// -----------------------------------------------------------------------
// <copyright file="IRepositoryDownloader.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

namespace Repository.Services
{
    /// <summary>
    /// Defines the <see cref="IRepositoryDownloader" />.
    /// </summary>
    public interface IRepositoryDownloader
    {
        /// <summary>
        /// The DownloadFileAsync.
        /// </summary>
        /// <param name="address">The address<see cref="string"/>.</param>
        /// <param name="outputFile">The outputFile<see cref="string"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        Task DownloadFileAsync(string address, string outputFile);
    }
}
