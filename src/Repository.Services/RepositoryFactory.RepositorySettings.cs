// -----------------------------------------------------------------------
// <copyright file="RepositoryFactory.RepositorySettings.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Repository.Services
{
    /// <summary>
    /// Defines the <see cref="RepositoryFactory" />.
    /// </summary>
    public static partial class RepositoryFactory
    {
        /// <summary>
        /// Defines the <see cref="RepositorySettings" />.
        /// </summary>
        private class RepositorySettings : IRepositorySettings
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="RepositorySettings"/> class.
            /// </summary>
            /// <param name="repositoryName">The repositoryName<see cref="string"/>.</param>
            /// <param name="solutionName">The solutionName<see cref="string"/>.</param>
            /// <param name="outputPath">The outputPath<see cref="string"/>.</param>
            /// <param name="rootNamespace">The rootNamespace<see cref="string"/>.</param>
            /// <param name="targetFramework">The targetFramework<see cref="string"/>.</param>
            /// <param name="projects">The projects<see cref="IEnumerable{IRepositoryProject}"/>.</param>
            public RepositorySettings(string repositoryName, string solutionName, string outputPath, string rootNamespace, string targetFramework, IEnumerable<IRepositoryProject> projects)
            {
                RepositoryName = repositoryName;
                SolutionName = solutionName;
                OutputPath = outputPath;
                RootNamespace = rootNamespace;
                TargetFramework = targetFramework;
                Projects = new List<IRepositoryProject>(projects);
            }

            /// <summary>
            /// Gets the OutputPath.
            /// </summary>
            public string OutputPath { get; }

            /// <summary>
            /// Gets the Projects.
            /// </summary>
            public IReadOnlyList<IRepositoryProject> Projects { get; }

            /// <summary>
            /// Gets the RepositoryName.
            /// </summary>
            public string RepositoryName { get; }

            /// <summary>
            /// Gets the RootNamespace.
            /// </summary>
            public string RootNamespace { get; }

            /// <summary>
            /// Gets the SolutionName.
            /// </summary>
            public string SolutionName { get; }

            /// <summary>
            /// Gets the TargetFramework.
            /// </summary>
            public string TargetFramework { get; }
        }
    }
}
