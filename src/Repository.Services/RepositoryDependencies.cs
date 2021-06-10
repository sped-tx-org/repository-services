// -----------------------------------------------------------------------
// <copyright file="RepositoryDependencies.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Repository.Services
{
    /// <summary>
    /// Defines the <see cref="RepositoryDependencies" />.
    /// </summary>
    public class RepositoryDependencies
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryDependencies"/> class.
        /// </summary>
        /// <param name="fileSystem">The fileSystem<see cref="IRepositoryFileSystem"/>.</param>
        /// <param name="downloader">The downloader<see cref="IRepositoryDownloader"/>.</param>
        /// <param name="expander">The expander<see cref="IRepositoryExpander"/>.</param>
        /// <param name="opener">The opener<see cref="IRepositoryOpener"/>.</param>
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

        /// <summary>
        /// Gets the Downloader.
        /// </summary>
        public IRepositoryDownloader Downloader { get; }

        /// <summary>
        /// Gets the Expander.
        /// </summary>
        public IRepositoryExpander Expander { get; }

        /// <summary>
        /// Gets the FileSystem.
        /// </summary>
        public IRepositoryFileSystem FileSystem { get; }

        /// <summary>
        /// Gets the Opener.
        /// </summary>
        public IRepositoryOpener Opener { get; }
    }
}
