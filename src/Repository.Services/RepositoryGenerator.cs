using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Repository.Services.MSBuild;

namespace Repository.Services
{

    internal class RepositoryGenerator : IRepositoryGenerator
    {
        public RepositoryGenerator(RepositoryDependencies dependencies)
        {
            Dependencies = dependencies;
        }

        public RepositoryDependencies Dependencies { get; }


        public async Task CreateRepositoryAsync(IRepositorySettings settings, CancellationToken cancellationToken = default)
        {
            string zipFileName = Guid.NewGuid().ToString("N");
            string zipFilePath = Path.Combine(settings.OutputPath, $"{zipFileName}.zip");
            string targetDirectory = Path.Combine(settings.OutputPath, settings.RepositoryName);
            var arcadeRepoDirectory = Path.Combine(settings.OutputPath, settings.RepositoryName, "src\\ArcadeRepo");

            Dependencies.FileSystem.CreateDirectory(targetDirectory);

            await Dependencies.Downloader.DownloadFileAsync(DownloadUrls.Arcade, zipFilePath);

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

        }

        

        private void CreateProjectDirectory(IRepositorySettings settings, IRepositoryProject project)
        {
            var root = ProjectFileFactory.Create(project);
            var newProjectDirectory = Path.Combine(settings.OutputPath, settings.RepositoryName, $"src\\{project.ProjectName}");
            var newProjectFilePath = Path.Combine(newProjectDirectory, $"{project.ProjectName}.csproj");
            Dependencies.FileSystem.CreateDirectory(newProjectDirectory);
            root.Save(newProjectFilePath);
        }

        //private void CreateProjectDirectory(IRepositorySettings settings, IRepositoryProject project)
        //{
        //    var replacements = RepositoryFactory.CreateReplacementsDictionary(settings);
        //    var arcadeRepoDirectory = Path.Combine(settings.OutputPath, settings.RepositoryName, "src\\ArcadeRepo");
        //    var newProjectDirectory = Path.Combine(settings.OutputPath, settings.RepositoryName, $"src\\{project.ProjectName}");

        //    Dependencies.FileSystem.CopyDirectory(arcadeRepoDirectory, newProjectDirectory);

        //    var oldProjectFilePath = Path.Combine(newProjectDirectory, "ArcadeRepo.csproj");
        //    var newProjectFilePath = Path.Combine(newProjectDirectory, $"{project.ProjectName}.csproj");

        //    File.Move(oldProjectFilePath, newProjectFilePath);

        //    var file = new FileInfo(newProjectFilePath);

        //    Dependencies.Replacer.ExecuteReplacements(file, replacements);

        //}

        //private void RenameSolutionFile(IRepositorySettings settings, Dictionary<string, string> replacements)
        //{
        //    string targetDirectory = Path.Combine(settings.OutputPath, settings.RepositoryName);
        //    string solutionFilePath = Path.Combine(targetDirectory, $"{settings.SolutionName}.sln");
        //    foreach (var file in Dependencies.FileSystem.GetFiles(targetDirectory))
        //    {
                
        //        switch (file.Name)
        //        {
        //            case "ArcadeRepo.sln":
        //                {
        //                    Dependencies.Replacer.ExecuteReplacements(file, replacements);
        //                    File.Move(file.FullName, solutionFilePath);
        //                    break;
        //                }

        //        }
        //    }
        //}
    }
}
