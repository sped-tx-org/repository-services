// -----------------------------------------------------------------------
// <copyright file="ProjectGuids.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace Repository.Services.MSBuild
{
    /// <summary>
    /// Defines the <see cref="ProjectGuids" />.
    /// </summary>
    internal static class ProjectGuids
    {
        /// <summary>
        /// Defines the CSharpGuid.
        /// </summary>
        public static Guid CSharpGuid = Guid.Parse(CSharpString);
        /// <summary>
        /// Defines the SolutionFolderGuid.
        /// </summary>
        public static Guid SolutionFolderGuid = Guid.Parse(SolutionFolderString);
        /// <summary>
        /// Defines the CSharpString.
        /// </summary>
        public const string CSharpString = "{9A19103F-16F7-4668-BE54-9A1E7A4F7556}";

        /// <summary>
        /// Defines the SolutionFolderString.
        /// </summary>
        public const string SolutionFolderString = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}";
    }
}
