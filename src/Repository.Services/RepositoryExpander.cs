// -----------------------------------------------------------------------
// <copyright file="RepositoryExpander.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Services
{
    /// <summary>
    /// Defines the <see cref="RepositoryExpander" />.
    /// </summary>
    public class RepositoryExpander : IRepositoryExpander
    {
        /// <summary>
        /// Defines the _fileSystem.
        /// </summary>
        private readonly IRepositoryFileSystem _fileSystem;
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryExpander"/> class.
        /// </summary>
        /// <param name="fileSystem">The fileSystem<see cref="IRepositoryFileSystem"/>.</param>
        public RepositoryExpander(IRepositoryFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// The ExpandZipFileAsync.
        /// </summary>
        /// <param name="zipFile">The zipFile<see cref="string"/>.</param>
        /// <param name="outputDirectory">The outputDirectory<see cref="string"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task ExpandZipFileAsync(string zipFile, string outputDirectory)
        {
            string tempGuid = Guid.NewGuid().ToString("N");
            string tempZipDir =
                Path.Combine(Path.GetDirectoryName(zipFile), tempGuid);
            ZipFile.ExtractToDirectory(zipFile, tempZipDir);
            var firstDirectory = Directory.GetDirectories(tempZipDir).SingleOrDefault();
            try
            {
                _fileSystem.CopyDirectory(firstDirectory, outputDirectory, true, true);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            _fileSystem.DeleteFileOrDirectory(tempZipDir);
            _fileSystem.DeleteFileOrDirectory(zipFile);

            await Task.CompletedTask;
        }
    }
}
