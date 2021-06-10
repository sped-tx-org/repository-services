// -----------------------------------------------------------------------
// <copyright file="RepositoryOpener.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;
using System.Threading.Tasks;

namespace Repository.Services
{
    /// <summary>
    /// Defines the <see cref="RepositoryOpener" />.
    /// </summary>
    public class RepositoryOpener : IRepositoryOpener
    {
        /// <summary>
        /// The CollapseSolutionAsync.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task CollapseSolutionAsync()
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// The OpenRepositoryAsync.
        /// </summary>
        /// <param name="solutionFilePath">The solutionFilePath<see cref="string"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task OpenRepositoryAsync(string solutionFilePath)
        {
            Process.Start(solutionFilePath);
            await Task.CompletedTask;
        }
    }
}
