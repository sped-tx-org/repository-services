// -----------------------------------------------------------------------
// <copyright file="RepositoryFactory.cs" company="sped-tx.net">
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
        /// The Settings.
        /// </summary>
        /// <param name="repositoryName">The repositoryName<see cref="string"/>.</param>
        /// <param name="solutionName">The solutionName<see cref="string"/>.</param>
        /// <param name="outputPath">The outputPath<see cref="string"/>.</param>
        /// <param name="rootNamespace">The rootNamespace<see cref="string"/>.</param>
        /// <param name="targetFramework">The targetFramework<see cref="string"/>.</param>
        /// <param name="projects">The projects<see cref="IEnumerable{IRepositoryProject}"/>.</param>
        /// <returns>The <see cref="IRepositorySettings"/>.</returns>
        public static IRepositorySettings Settings(string repositoryName,
            string solutionName,
            string outputPath,
            string rootNamespace,
            string targetFramework,
            IEnumerable<IRepositoryProject> projects)
        {
            return new RepositorySettings(repositoryName,
                solutionName,
                outputPath,
                rootNamespace,
                targetFramework,
                projects);
        }

        /// <summary>
        /// The Project.
        /// </summary>
        /// <param name="projectName">The projectName<see cref="string"/>.</param>
        /// <param name="rootNamespace">The rootNamespace<see cref="string"/>.</param>
        /// <param name="targetFramework">The targetFramework<see cref="string"/>.</param>
        /// <param name="outputType">The outputType<see cref="string"/>.</param>
        /// <returns>The <see cref="IRepositoryProject"/>.</returns>
        public static IRepositoryProject Project(string projectName,
            string rootNamespace,
            string targetFramework,
            string outputType)
        {
            return new RepositoryProject(projectName, rootNamespace, targetFramework, outputType);
        }
    }
}
