// -----------------------------------------------------------------------
// <copyright file="IRepositoryGenerator.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;

namespace Repository.Services
{
    /// <summary>
    /// Defines the <see cref="IRepositoryGenerator" />.
    /// </summary>
    public interface IRepositoryGenerator
    {
        /// <summary>
        /// The CreateRepositoryAsync.
        /// </summary>
        /// <param name="settings">The settings<see cref="IRepositorySettings"/>.</param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        Task CreateRepositoryAsync(IRepositorySettings settings, CancellationToken cancellationToken = default);
    }
}
