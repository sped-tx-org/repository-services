using System.Collections.Generic;

namespace Repository.Services
{
    public interface IRepositorySettings
    {
        string RepositoryName { get; }
        string SolutionName { get; }
        string OutputPath { get; }
        string RootNamespace { get; }
        string TargetFramework { get; }

        IReadOnlyList<IRepositoryProject> Projects { get; }
    }
}
