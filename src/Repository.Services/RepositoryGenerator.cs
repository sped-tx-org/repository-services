// -----------------------------------------------------------------------
// <copyright file="RepositoryGenerator.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Repository.Services.MSBuild;

namespace Repository.Services
{
    /// <summary>
    /// Defines the <see cref="RepositoryGenerator" />.
    /// </summary>
    public class RepositoryGenerator : IRepositoryGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryGenerator"/> class.
        /// </summary>
        /// <param name="dependencies">The dependencies<see cref="RepositoryDependencies"/>.</param>
        public RepositoryGenerator(RepositoryDependencies dependencies)
        {
            Dependencies = dependencies;
        }

        /// <summary>
        /// Gets the Dependencies.
        /// </summary>
        public RepositoryDependencies Dependencies { get; }

        /// <summary>
        /// The CreateRepositoryAsync.
        /// </summary>
        /// <param name="settings">The settings<see cref="IRepositorySettings"/>.</param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task CreateRepositoryAsync(IRepositorySettings settings, CancellationToken cancellationToken = default)
        {
            string zipFileName = Guid.NewGuid().ToString("N");
            string zipFilePath = Path.Combine(settings.OutputPath, $"{zipFileName}.zip");
            string targetDirectory = Path.Combine(settings.OutputPath, settings.RepositoryName);
            var arcadeRepoDirectory = Path.Combine(settings.OutputPath, settings.RepositoryName, "src\\ArcadeRepo");

            Dependencies.FileSystem.CreateDirectory(targetDirectory);

            await Dependencies.Downloader.DownloadFileAsync(settings.RepoZipFileAddress, zipFilePath);

            await Dependencies.Expander.ExpandZipFileAsync(zipFilePath, targetDirectory);

            foreach (var project in settings.Projects)
            {
                CreateProjectDirectory(settings, project);
            }

            Dependencies.FileSystem.DeleteFileOrDirectory(arcadeRepoDirectory);
            Dependencies.FileSystem.DeleteFileOrDirectory(Path.Combine(targetDirectory, "ArcadeRepo.sln"));
            Dependencies.FileSystem.DeleteFileOrDirectory(Path.Combine(targetDirectory, "tools\\CodeGenerator"));


            string solutionFilePath = Path.Combine(targetDirectory, $"{settings.SolutionName}.sln");

            var solutionFile = SolutionFile.From(settings);

            solutionFile.Save(solutionFilePath);

            await Dependencies.Opener.OpenRepositoryAsync(solutionFilePath);

            await Dependencies.Opener.CollapseSolutionAsync();
        }

        /// <summary>
        /// The CreateProjectDirectory.
        /// </summary>
        /// <param name="settings">The settings<see cref="IRepositorySettings"/>.</param>
        /// <param name="project">The project<see cref="IRepositoryProject"/>.</param>
        private void CreateProjectDirectory(IRepositorySettings settings, IRepositoryProject project)
        {
            var root = ProjectFileFactory.Create(project);
            var newProjectDirectory = Path.Combine(settings.OutputPath, settings.RepositoryName, $"src\\{project.ProjectName}");
            var newProjectFilePath = Path.Combine(newProjectDirectory, $"{project.ProjectName}.csproj");
            Dependencies.FileSystem.CreateDirectory(newProjectDirectory);
            root.Save(newProjectFilePath);
        }
    }
}
