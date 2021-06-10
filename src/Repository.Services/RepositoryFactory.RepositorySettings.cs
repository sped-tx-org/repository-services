using System.Collections.Generic;

namespace Repository.Services
{
    public static partial class RepositoryFactory
    {
        private class RepositorySettings : IRepositorySettings
        {

            public RepositorySettings(string repositoryName, string solutionName, string outputPath, string rootNamespace, string targetFramework, IEnumerable<IRepositoryProject> projects)
            {
                RepositoryName = repositoryName;
                SolutionName = solutionName;
                OutputPath = outputPath;
                RootNamespace = rootNamespace;
                TargetFramework = targetFramework;
                Projects = new List<IRepositoryProject>(projects);
            }

            public string RepositoryName { get; }
            public string SolutionName { get; }
            public string OutputPath { get; }
            public string RootNamespace { get; }
            public string TargetFramework { get; }

            public IReadOnlyList<IRepositoryProject> Projects { get; }
        }
    }
}
