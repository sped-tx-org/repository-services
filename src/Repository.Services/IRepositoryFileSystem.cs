// -----------------------------------------------------------------------
// <copyright file="IRepositoryFileSystem.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;

namespace Repository.Services
{
    /// <summary>
    /// Defines the <see cref="IRepositoryFileSystem" />.
    /// </summary>
    public interface IRepositoryFileSystem
    {
        /// <summary>
        /// The GetFiles.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <returns>The <see cref="IEnumerable{FileInfo}"/>.</returns>
        IEnumerable<FileInfo> GetFiles(string path);

        /// <summary>
        /// The CreateDirectory.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        void CreateDirectory(string path);

        /// <summary>
        /// The DeleteFileOrDirectory.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="recurse">The recurse<see cref="bool"/>.</param>
        void DeleteFileOrDirectory(string path, bool recurse = true);

        /// <summary>
        /// The CopyDirectory.
        /// </summary>
        /// <param name="source">The source<see cref="string"/>.</param>
        /// <param name="destination">The destination<see cref="string"/>.</param>
        /// <param name="overwrite">The overwrite<see cref="bool"/>.</param>
        /// <param name="deleteSourceOnCompletion">The deleteSourceOnCompletion<see cref="bool"/>.</param>
        void CopyDirectory(string source, string destination, bool overwrite = false, bool deleteSourceOnCompletion = false);
    }
}
