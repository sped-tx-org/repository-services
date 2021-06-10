// -----------------------------------------------------------------------
// <copyright file="SolutionProject.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

namespace Repository.Services.MSBuild
{
    /// <summary>
    /// Defines the <see cref="SolutionProject" />.
    /// </summary>
    public class SolutionProject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionProject"/> class.
        /// </summary>
        /// <param name="projectTypeGuid">The projectTypeGuid<see cref="Guid"/>.</param>
        /// <param name="projectName">The projectName<see cref="string"/>.</param>
        /// <param name="relativePath">The relativePath<see cref="string"/>.</param>
        /// <param name="projectGuid">The projectGuid<see cref="Guid"/>.</param>
        /// <param name="solutionItems">The solutionItems<see cref="Dictionary{string, string}"/>.</param>
        public SolutionProject(Guid projectTypeGuid, string projectName, string relativePath, Guid projectGuid, Dictionary<string, string> solutionItems = null)
        {
            ProjectTypeGuid = projectTypeGuid;
            ProjectName = projectName;
            RelativePath = relativePath;
            ProjectGuid = projectGuid;
            if (solutionItems != null)
            {
                SolutionItems = new SolutionSection("Project", "SolutionItems", "preProject", solutionItems);
            }
        }

        /// <summary>
        /// Gets a value indicating whether HasSolutionItems.
        /// </summary>
        public bool HasSolutionItems =>
            SolutionItems?.Values.Count > 0;

        /// <summary>
        /// Gets the ProjectGuid.
        /// </summary>
        public Guid ProjectGuid { get; }

        /// <summary>
        /// Gets the ProjectName.
        /// </summary>
        public string ProjectName { get; }

        /// <summary>
        /// Gets the ProjectTypeGuid.
        /// </summary>
        public Guid ProjectTypeGuid { get; }

        /// <summary>
        /// Gets the RelativePath.
        /// </summary>
        public string RelativePath { get; }

        /// <summary>
        /// Gets the SolutionItems.
        /// </summary>
        public SolutionSection SolutionItems { get; }

        /// <summary>
        /// Gets the ProjectGuidString.
        /// </summary>
        internal string ProjectGuidString => ProjectGuid.ToString("B").ToUpper();

        /// <summary>
        /// Gets the ProjectTypeGuidString.
        /// </summary>
        internal string ProjectTypeGuidString => ProjectTypeGuid.ToString("B").ToUpper();

        /// <summary>
        /// The WriteTo.
        /// </summary>
        /// <param name="writer">The writer<see cref="TextWriter"/>.</param>
        public void WriteTo(TextWriter writer)
        {
            var projectTypeGuid = ProjectTypeGuid.ToString("B").ToUpper();
            var projectGuid = ProjectGuid.ToString("B").ToUpper();
            writer.WriteLine($"Project(\"{projectTypeGuid}\") = \"{ProjectName}\", \"{RelativePath}\", \"{projectGuid}\"");
            if (HasSolutionItems)
            {
                writer.WriteLine($"\tProjectSection({SolutionItems.Name}) = {SolutionItems.State}");
                foreach (var kvp in SolutionItems.Values)
                {
                    writer.WriteLine($"\t\t{kvp.Key} = {kvp.Value}");
                }

                writer.WriteLine("\tEndProjectSection");

            }

            writer.WriteLine("EndProject");
        }
    }
}
