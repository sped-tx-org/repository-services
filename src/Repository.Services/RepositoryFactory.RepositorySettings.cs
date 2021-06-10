using System.Collections.Generic;

namespace Repository.Services
{
    public static partial class RepositoryFactory
    {
        private class RepositorySettings : IRepositorySettings
        {
            public RepositorySettings(string repositoryName,
                string solutionName,
                string outputPath,
                string rootNamespace,
                string targetFramework,
                string repoZipFileAddress,
                IEnumerable<IRepositoryProject> projects)
            {
                RepositoryName = repositoryName;
                SolutionName = solutionName;
                OutputPath = outputPath;
                RootNamespace = rootNamespace;
                TargetFramework = targetFramework;
                RepoZipFileAddress = repoZipFileAddress;
                Projects = new List<IRepositoryProject>(projects);
            }

            public string OutputPath { get; }

            public IReadOnlyList<IRepositoryProject> Projects { get; }

            public string RepositoryName { get; }

            public string RootNamespace { get; }

            public string SolutionName { get; }

            public string TargetFramework { get; }

            public string RepoZipFileAddress { get; }
        }
    }
}
