using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{

    internal class RepositoryFileSystem : IRepositoryFileSystem
    {
        public void CopyDirectory(string source, string destination, bool overwrite = false, bool deleteSourceOnCompletion = false)
        {
            DirectoryInfo dir = new DirectoryInfo(source);

            if (!dir.Exists)
                throw new DirectoryNotFoundException(source);

            var dirs = dir.GetDirectories();

            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            foreach (FileInfo file in dir.GetFiles())
            {
                string targetPath = Path.Combine(destination, file.Name);
                file.CopyTo(targetPath, overwrite);
            }

            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destination, subdir.Name);
                CopyDirectory(subdir.FullName, temppath, overwrite, deleteSourceOnCompletion);
            }

            if (deleteSourceOnCompletion)
                DeleteFileOrDirectory(source);
        }


        public void DeleteFileOrDirectory(string path, bool recurse = true)
        {
            if (IsDirectory(path))
            {
                Directory.Delete(path, recurse);
            }
            else
            {
                File.Delete(path);
            }
        }

        private static bool IsDirectory(string path)
        {
            FileAttributes attr = File.GetAttributes(path);

            return (attr & FileAttributes.Directory) == FileAttributes.Directory;
        }

        public void CreateDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, true);
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                    return;
                }
            }

            Directory.CreateDirectory(path);
        }

        public IEnumerable<FileInfo> GetFiles(string path)
        {
            foreach(var filePath in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
            {
                yield return new FileInfo(filePath);
            }
        }
    }
}
