namespace Repository.Services
{
    public interface IRepositoryProject
    {
        string ProjectName { get; }
        string RootNamespace { get; }
        string TargetFramework { get; }
        string OutputType { get; }
    }
}
