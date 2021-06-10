// -----------------------------------------------------------------------
// <copyright file="RepositoryFactory.RepositoryProject.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Repository.Services
{
    /// <summary>
    /// Defines the <see cref="RepositoryFactory" />.
    /// </summary>
    public static partial class RepositoryFactory
    {
        /// <summary>
        /// Defines the <see cref="RepositoryProject" />.
        /// </summary>
        private class RepositoryProject : IRepositoryProject
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="RepositoryProject"/> class.
            /// </summary>
            /// <param name="projectName">The projectName<see cref="string"/>.</param>
            /// <param name="rootNamespace">The rootNamespace<see cref="string"/>.</param>
            /// <param name="targetFramework">The targetFramework<see cref="string"/>.</param>
            /// <param name="outputType">The outputType<see cref="string"/>.</param>
            public RepositoryProject(string projectName, string rootNamespace, string targetFramework, string outputType)
            {
                ProjectName = projectName;
                RootNamespace = rootNamespace;
                TargetFramework = targetFramework;
                OutputType = outputType;
            }

            /// <summary>
            /// Gets the OutputType.
            /// </summary>
            public string OutputType { get; }

            /// <summary>
            /// Gets the ProjectName.
            /// </summary>
            public string ProjectName { get; }

            /// <summary>
            /// Gets the RootNamespace.
            /// </summary>
            public string RootNamespace { get; }

            /// <summary>
            /// Gets the TargetFramework.
            /// </summary>
            public string TargetFramework { get; }
        }
    }
}
