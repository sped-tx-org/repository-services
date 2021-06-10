namespace Repository.Services
{

    public class RepositoryDependencies
    {
        public RepositoryDependencies(IRepositoryFileSystem fileSystem,
            IRepositoryDownloader downloader,
            IRepositoryExpander expander,
            IRepositoryOpener opener)
        {
            FileSystem = fileSystem;
            Downloader = downloader;
            Expander = expander;
            Opener = opener;
        }

        public IRepositoryFileSystem FileSystem { get; }

        public IRepositoryDownloader Downloader { get; }

        public IRepositoryExpander Expander { get; }

        public IRepositoryOpener Opener { get; }

    }
}
