namespace Repository.Services
{
    public static partial class RepositoryFactory
    {
        private class RepositoryProject : IRepositoryProject
        {
            public RepositoryProject(string projectName, string rootNamespace, string targetFramework, string outputType)
            {
                ProjectName = projectName;
                RootNamespace = rootNamespace;
                TargetFramework = targetFramework;
                OutputType = outputType;
            }

            public string ProjectName { get; }
            public string RootNamespace { get; }
            public string TargetFramework { get; }
            public string OutputType { get; }
        }
    }
}
