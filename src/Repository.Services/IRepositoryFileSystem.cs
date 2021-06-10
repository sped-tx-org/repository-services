using System.Collections.Generic;
using System.IO;

namespace Repository.Services
{
    public interface IRepositoryFileSystem
    {
        IEnumerable<FileInfo> GetFiles(string path);
        void CreateDirectory(string path);
        void DeleteFileOrDirectory(string path, bool recurse = true);
        void CopyDirectory(string source, string destination, bool overwrite = false, bool deleteSourceOnCompletion = false);
    }
}
