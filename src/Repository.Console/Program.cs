using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.CommandLineUtils;
using Repository.Services;

namespace Repository
{
    public static class Program
    {
        private const string HelpTemplate = "-?|-h|--help";
        private const string SolutionNameTemplate = "-s|--solution-name";
        private const string TargetFrameworkTemplate = "-t|--target-framework";
        private const string RootNamespaceTemplate = "-r|--root-namespace";


        private const string WorkingDirectoryDescription = "Specifies the enclosing directory for the repository root directory.";
        private const string RepositoryNameDescription = "The name of the repository";
        private const string SolutionNameDescription = "The name of the solution file";
        private const string ProjectNamesDescription = "Supplies the names of the projects to create in the repository";
        private const string TargetFrameworkDescription = "";
        private const string RootNamespaceDescription = "";
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var app = new CommandLineApplication();
            app.Name = "Repository Console";
            app.Description = "Generates out-of-the-box ready-to-go repositories for .NET";
            app.HelpOption(HelpTemplate);
            app.VersionOption("-v|--version", () => {
                return string.Format("Version {0}", Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
            });

            var repositoryNameArgument = app.Argument("Repository Name", RepositoryNameDescription);
            var workingDirectoryArgument = app.Argument("Working Directory", WorkingDirectoryDescription);
            var projectsArgument = app.Argument("Project Names", ProjectNamesDescription, true);

            var solutionNameOption = app.Option(SolutionNameTemplate, SolutionNameDescription, CommandOptionType.SingleValue);
            var targetFrameworkOption = app.Option(TargetFrameworkTemplate, TargetFrameworkDescription, CommandOptionType.SingleValue);
            var rootNamespaceOption = app.Option(RootNamespaceTemplate, RootNamespaceDescription, CommandOptionType.SingleValue);

            app.OnExecute(() =>
            {
                var repositoryName = repositoryNameArgument.Value;
                var workingDirectory = workingDirectoryArgument.Value;
                var projectNames = projectsArgument.Values;
                var solutionName = solutionNameOption.HasValue() ? solutionNameOption.Value() : repositoryName;
                var targetFramework = targetFrameworkOption.HasValue() ? targetFrameworkOption.Value() : "net48";
                var rootNamespace = rootNamespaceOption.HasValue() ? rootNamespaceOption.Value() : repositoryName;

                Console.WriteLine($"Repository Name: {repositoryName}");
                Console.WriteLine($"Working Directory: {workingDirectory}");
                Console.WriteLine($"Solution Name: {solutionName}");
                Console.WriteLine($"Repository Name: {repositoryName}");
                Console.WriteLine($"Target Framework: {targetFramework}");
                Console.WriteLine($"Root Namespace: {rootNamespace}");
                Console.WriteLine($"Project Names: {string.Join(",", projectNames)}");

                var generator = RepositoryServices.GetService<IRepositoryGenerator>();

                var projects = new List<IRepositoryProject>();
                foreach(var projectName in projectNames)
                {
                    projects.Add(RepositoryFactory.Project(projectName, rootNamespace, targetFramework, "Library"));
                }

                var settings = RepositoryFactory.Settings(repositoryName, solutionName, workingDirectory, rootNamespace, targetFramework, projects);

                generator.CreateRepositoryAsync(settings);


                return 0;
            });


            try
            {
                // This begins the actual execution of the application
                Console.WriteLine("Repository Console Initializing...");
                app.Execute(args);
            }
            catch (CommandParsingException ex)
            {
                // You'll always want to catch this exception, otherwise it will generate a messy and confusing error for the end user.
                // the message will usually be something like:
                // "Unrecognized command or argument '<invalid-command>'"
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("Unable to execute application: {0}", ex.Message + " " + ex.StackTrace);
            }
        }
    }
}
