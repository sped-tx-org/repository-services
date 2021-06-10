using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{

    internal class RepositoryExpander : IRepositoryExpander
    {
        private readonly IRepositoryFileSystem _fileSystem;

        public RepositoryExpander(IRepositoryFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

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
