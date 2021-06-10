// -----------------------------------------------------------------------
// <copyright file="IRepositorySettings.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Repository.Services
{
    /// <summary>
    /// Defines the <see cref="IRepositorySettings" />.
    /// </summary>
    public interface IRepositorySettings
    {
        /// <summary>
        /// Gets the OutputPath.
        /// </summary>
        string OutputPath { get; }

        /// <summary>
        /// Gets the Projects.
        /// </summary>
        IReadOnlyList<IRepositoryProject> Projects { get; }

        /// <summary>
        /// Gets the RepositoryName.
        /// </summary>
        string RepositoryName { get; }

        /// <summary>
        /// Gets the RootNamespace.
        /// </summary>
        string RootNamespace { get; }

        /// <summary>
        /// Gets the SolutionName.
        /// </summary>
        string SolutionName { get; }

        /// <summary>
        /// Gets the TargetFramework.
        /// </summary>
        string TargetFramework { get; }
    }
}
