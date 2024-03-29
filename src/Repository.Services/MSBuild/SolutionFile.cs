﻿// -----------------------------------------------------------------------
// <copyright file="SolutionFile.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Repository.Services.MSBuild
{
    /// <summary>
    /// Defines the <see cref="SolutionFile" />.
    /// </summary>
    public class SolutionFile
    {
        /// <summary>
        /// Defines the _globalSections.
        /// </summary>
        private readonly List<SolutionSection> _globalSections = new List<SolutionSection>();
        /// <summary>
        /// Defines the _projects.
        /// </summary>
        private readonly List<SolutionProject> _projects = new List<SolutionProject>();
        /// <summary>
        /// Gets the FileVersion.
        /// </summary>
        public string FileVersion => "12.00";

        /// <summary>
        /// Gets the GlobalSections.
        /// </summary>
        public IReadOnlyList<SolutionSection> GlobalSections => _globalSections;

        /// <summary>
        /// Gets the MinimumVisualStudioVersion.
        /// </summary>
        public string MinimumVisualStudioVersion => "10.0.40219.1";

        /// <summary>
        /// Gets the Projects.
        /// </summary>
        public IReadOnlyList<SolutionProject> Projects => _projects;

        /// <summary>
        /// Gets the VisualStudioVersion.
        /// </summary>
        public string VisualStudioVersion => "16.0.29006.145";

        /// <summary>
        /// The From.
        /// </summary>
        /// <param name="settings">The settings<see cref="IRepositorySettings"/>.</param>
        /// <returns>The <see cref="SolutionFile"/>.</returns>
        public static SolutionFile From(IRepositorySettings settings)
        {
            var solution = new SolutionFile();
            var build = solution.AddSolutionFolderProject("Build", new Dictionary<string, string>
            {
                {".editorconfig",".editorconfig"},
                {".gitignore",".gitignore"},
                {"Directory.Build.props","Directory.Build.props"},
                {"Directory.Build.targets","Directory.Build.targets"},
                {"global.json","global.json"},
                {"NuGet.config","NuGet.config"},
                {"README.md","README.md"},
                {"eng\\Version.Details.xml","eng\\Version.Details.xml"},
                {"eng\\Versions.props","eng\\Versions.props"},
                {"eng\\VisualStudio.props","eng\\VisualStudio.props"},
                {"eng\\VisualStudio.targets","eng\\VisualStudio.targets"},
            });
            var tools = solution.AddSolutionFolderProject("Tools");
            var map = new Dictionary<string, Guid>();
            var projectConfigurationPlatforms = new Dictionary<string, string>();
            foreach (var project in settings.Projects)
            {
                var projectName = project.ProjectName;
                var newProject = solution.AddCSharpProject(projectName, $"src\\{projectName}\\{projectName}.csproj");
                map[projectName] = newProject.ProjectGuid;
                projectConfigurationPlatforms[$"{newProject.ProjectGuidString}.Debug|Any CPU.ActiveCf"] = "Debug|Any CPU";
                projectConfigurationPlatforms[$"{newProject.ProjectGuidString}.Debug|Any CPU.Build.0"] = "Debug|Any CPU";
                projectConfigurationPlatforms[$"{newProject.ProjectGuidString}.Release|Any CPU.ActiveCf"] = "Release|Any CPU";
                projectConfigurationPlatforms[$"{newProject.ProjectGuidString}.Release|Any CPU.Build.0"] = "Release|Any CPU";

            }
            solution.AddGlobalSection("SolutionConfigurationPlatforms", "preSolution", new Dictionary<string, string>
            {
                {"Debug|Any CPU", "Debug|Any CPU"},
                {"Release|Any CPU", "Release|Any CPU"},
            });
            solution.AddGlobalSection("ProjectConfigurationPlatforms", "postSolution", projectConfigurationPlatforms);
            solution.AddGlobalSection("SolutionProperties", "preSolution", new Dictionary<string, string>
            {
                {"HideSolutionNode", "FALSE"}
            });
            solution.AddGlobalSection("ExtensibilityGlobals", "postSolution", new Dictionary<string, string>
            {
                {"SolutionGuid", Guid.NewGuid().ToString("B").ToUpper()}
            });
            return solution;
        }

        /// <summary>
        /// The AddCSharpProject.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="relativePath">The relativePath<see cref="string"/>.</param>
        /// <returns>The <see cref="SolutionProject"/>.</returns>
        public SolutionProject AddCSharpProject(string name, string relativePath)
        {
            var projectTypeGuid = ProjectGuids.CSharpGuid;
            var projectGuid = Guid.NewGuid();

            var project = new SolutionProject(projectTypeGuid, name, relativePath, projectGuid);

            _projects.Add(project);

            return project;
        }

        /// <summary>
        /// The AddSolutionFolderProject.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="items">The items<see cref="Dictionary{string, string}"/>.</param>
        /// <returns>The <see cref="SolutionProject"/>.</returns>
        public SolutionProject AddSolutionFolderProject(string name, Dictionary<string, string> items = null)
        {
            var projectTypeGuid = ProjectGuids.SolutionFolderGuid;
            var projectGuid = Guid.NewGuid();
            var project = new SolutionProject(projectTypeGuid, name, name, projectGuid, items);
            _projects.Add(project);
            return project;
        }

        /// <summary>
        /// The AddGlobalSection.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="state">The state<see cref="string"/>.</param>
        /// <param name="items">The items<see cref="Dictionary{string, string}"/>.</param>
        /// <returns>The <see cref="SolutionSection"/>.</returns>
        public SolutionSection AddGlobalSection(string name, string state, Dictionary<string, string> items = null)
        {
            var section = new SolutionSection("Global", name, state, items);
            _globalSections.Add(section);
            return section;
        }

        /// <summary>
        /// The GetText.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetText()
        {
            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            WriteTo(writer);
            return builder.ToString();
        }

        /// <summary>
        /// The WriteTo.
        /// </summary>
        /// <param name="writer">The writer<see cref="TextWriter"/>.</param>
        public void WriteTo(TextWriter writer)
        {
            writer.WriteLine($"Microsoft Visual Studio Solution File, Format Version {FileVersion}");
            writer.WriteLine("# Visual Studio 16");
            writer.WriteLine($"VisualStudioVersion = {VisualStudioVersion}");
            writer.WriteLine($"MinimumVisualStudioVersion = {MinimumVisualStudioVersion}");
            foreach (var project in _projects)
            {
                project.WriteTo(writer);
            }
            writer.WriteLine("Global");
            foreach (var section in _globalSections)
            {
                section.WriteTo(writer);
            }
            writer.WriteLine("EndGlobal");
        }

        /// <summary>
        /// The Save.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        public void Save(string path)
        {
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            using (var writer = new StreamWriter(stream))
            {
                WriteTo(writer);
                writer.Flush();
            }
        }
    }
}
