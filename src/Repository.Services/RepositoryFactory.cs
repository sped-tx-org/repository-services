using System;
using System.Collections.Generic;

namespace Repository.Services
{
    public static partial class RepositoryFactory
    {
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

        public static IRepositoryProject Project(string projectName,
            string rootNamespace,
            string targetFramework,
            string outputType)
        {
            return new RepositoryProject(projectName, rootNamespace, targetFramework, outputType);
        }
    }
}
