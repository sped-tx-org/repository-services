// -----------------------------------------------------------------------
// <copyright file="IRepositoryOpener.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

namespace Repository.Services
{
    /// <summary>
    /// Defines the <see cref="IRepositoryOpener" />.
    /// </summary>
    public interface IRepositoryOpener
    {
        /// <summary>
        /// The CollapseSolutionAsync.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        Task CollapseSolutionAsync();

        /// <summary>
        /// The OpenRepositoryAsync.
        /// </summary>
        /// <param name="solutionFilePath">The solutionFilePath<see cref="string"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        Task OpenRepositoryAsync(string solutionFilePath);
    }
}
