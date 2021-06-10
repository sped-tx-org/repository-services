// -----------------------------------------------------------------------
// <copyright file="IRepositoryExpander.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

namespace Repository.Services
{
    /// <summary>
    /// Defines the <see cref="IRepositoryExpander" />.
    /// </summary>
    public interface IRepositoryExpander
    {
        /// <summary>
        /// The ExpandZipFileAsync.
        /// </summary>
        /// <param name="zipFile">The zipFile<see cref="string"/>.</param>
        /// <param name="outputDirectory">The outputDirectory<see cref="string"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        Task ExpandZipFileAsync(string zipFile, string outputDirectory);
    }
}
