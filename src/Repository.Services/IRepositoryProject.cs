// -----------------------------------------------------------------------
// <copyright file="IRepositoryProject.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Repository.Services
{
    /// <summary>
    /// Defines the <see cref="IRepositoryProject" />.
    /// </summary>
    public interface IRepositoryProject
    {
        /// <summary>
        /// Gets the OutputType.
        /// </summary>
        string OutputType { get; }

        /// <summary>
        /// Gets the ProjectName.
        /// </summary>
        string ProjectName { get; }

        /// <summary>
        /// Gets the RootNamespace.
        /// </summary>
        string RootNamespace { get; }

        /// <summary>
        /// Gets the TargetFramework.
        /// </summary>
        string TargetFramework { get; }

        /// <summary>
        /// Gets the ProjectType.
        /// </summary>
        ProjectType ProjectType { get; }
    }
}
