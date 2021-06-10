// -----------------------------------------------------------------------
// <copyright file="RepositoryFileSystem.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Repository.Services
{
    /// <summary>
    /// Defines the <see cref="RepositoryFileSystem" />.
    /// </summary>
    public class RepositoryFileSystem : IRepositoryFileSystem
    {
        /// <summary>
        /// The CopyDirectory.
        /// </summary>
        /// <param name="source">The source<see cref="string"/>.</param>
        /// <param name="destination">The destination<see cref="string"/>.</param>
        /// <param name="overwrite">The overwrite<see cref="bool"/>.</param>
        /// <param name="deleteSourceOnCompletion">The deleteSourceOnCompletion<see cref="bool"/>.</param>
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

        /// <summary>
        /// The DeleteFileOrDirectory.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="recurse">The recurse<see cref="bool"/>.</param>
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

        /// <summary>
        /// The IsDirectory.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private static bool IsDirectory(string path)
        {
            FileAttributes attr = File.GetAttributes(path);

            return (attr & FileAttributes.Directory) == FileAttributes.Directory;
        }

        /// <summary>
        /// The CreateDirectory.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
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

        /// <summary>
        /// The GetFiles.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <returns>The <see cref="IEnumerable{FileInfo}"/>.</returns>
        public IEnumerable<FileInfo> GetFiles(string path)
        {
            foreach (var filePath in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
            {
                yield return new FileInfo(filePath);
            }
        }
    }
}
