// -----------------------------------------------------------------------
// <copyright file="ProjectFileFactory.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Microsoft.Build.Construction;

namespace Repository.Services.MSBuild
{
    /// <summary>
    /// Defines the <see cref="ProjectFileFactory" />.
    /// </summary>
    internal static class ProjectFileFactory
    {
        /// <summary>
        /// Defines the LibraryOutputType.
        /// </summary>
        private const string LibraryOutputType = "Library";

        /// <summary>
        /// Defines the MicrosoftNETSdk.
        /// </summary>
        private const string MicrosoftNETSdk = "Microsoft.NET.Sdk";

        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="project">The project<see cref="IRepositoryProject"/>.</param>
        /// <returns>The <see cref="ProjectRootElement"/>.</returns>
        public static ProjectRootElement Create(IRepositoryProject project)
        {
            switch (project.ProjectType)
            {
                case ProjectType.VSPackage: return CreateVSPackageProject(project);
                case ProjectType.MEFLibrary: return CreateMEFLibraryProject(project);
                case ProjectType.Vsix: return CreateVsixProject(project);
                case ProjectType.MSBuildTask: return CreateMSBuildTaskProject(project);
                case ProjectType.PowerShellModule: return CreatePowerShellModuleProject(project);
                default: return CreateDefaultProject(project);
            }
        }

        private static ProjectRootElement CreatePowerShellModuleProject(IRepositoryProject project)
        {
            return CreateDefaultProject(project);
        }

        private static ProjectRootElement CreateMSBuildTaskProject(IRepositoryProject project)
        {
            return CreateDefaultProject(project);
        }

        private static ProjectRootElement CreateVsixProject(IRepositoryProject project)
        {
            ProjectRootElement root = ProjectRootElement.Create();
            var group0 = root.CreatePropertyGroupElement();
            root.AppendChild(group0);
            group0.AddProperty("IsPackable", "false");
            group0.AddProperty("UsingToolVsSDK", "true");
            group0.AddProperty("GeneratePkgDefFile", "false");
            group0.AddProperty("CreateVsixContainer", "true");
            group0.AddProperty("TargetVsixContainerName", $"{project.ProjectName}.vsix");
            root.AppendImportSdkProps();
            var group1 = root.CreatePropertyGroupElement();
            root.AppendChild(group1);
            group1.AddProperty("TargetFramework", project.TargetFramework);
            group1.AddProperty("RootNamespace", project.RootNamespace);
            if (project.OutputType != LibraryOutputType)
            {
                group1.AddProperty("OutputType", project.OutputType);
            }
            root.AppendImportSdkTargets();
            return root;
        }

        private static ProjectRootElement CreateMEFLibraryProject(IRepositoryProject project)
        {
            ProjectRootElement root = ProjectRootElement.Create();
            var group0 = root.CreatePropertyGroupElement();
            root.AppendChild(group0);
            group0.AddProperty("IsPackable", "false");
            group0.AddProperty("UsingToolVsSDK", "true");
            group0.AddProperty("GeneratePkgDefFile", "false");
            group0.AddProperty("CreateVsixContainer", "false");
            root.AppendImportSdkProps();
            var group1 = root.CreatePropertyGroupElement();
            root.AppendChild(group1);
            group1.AddProperty("TargetFramework", project.TargetFramework);
            group1.AddProperty("RootNamespace", project.RootNamespace);
            if (project.OutputType != LibraryOutputType)
            {
                group1.AddProperty("OutputType", project.OutputType);
            }
            root.AppendImportSdkTargets();
            return root;
        }

        private static ProjectRootElement CreateVSPackageProject(IRepositoryProject project)
        {
            ProjectRootElement root = ProjectRootElement.Create();
            var group0 = root.CreatePropertyGroupElement();
            root.AppendChild(group0);
            group0.AddProperty("IsPackable", "false");
            group0.AddProperty("UsingToolVsSDK", "true");
            group0.AddProperty("GeneratePkgDefFile", "true");
            group0.AddProperty("CreateVsixContainer", "false");
            root.AppendImportSdkProps();
            var group1 = root.CreatePropertyGroupElement();
            root.AppendChild(group1);
            group1.AddProperty("TargetFramework", project.TargetFramework);
            group1.AddProperty("RootNamespace", project.RootNamespace);
            if (project.OutputType != LibraryOutputType)
            {
                group1.AddProperty("OutputType", project.OutputType);
            }
            root.AppendImportSdkTargets();
            return root;
        }

        private static ProjectRootElement CreateDefaultProject(IRepositoryProject project)
        {
            ProjectRootElement root = ProjectRootElement.Create();

            var group0 = root.CreatePropertyGroupElement();
            root.AppendChild(group0);
            group0.AddProperty("IsPackable", "false");

            root.AppendImportSdkProps();

            var group1 = root.CreatePropertyGroupElement();
            root.AppendChild(group1);
            group1.AddProperty("TargetFramework", project.TargetFramework);
            group1.AddProperty("RootNamespace", project.RootNamespace);
            if (project.OutputType != LibraryOutputType)
            {
                group1.AddProperty("OutputType", project.OutputType);
            }

            root.AppendImportSdkTargets();

            return root;
        }

        private static ProjectImportElement AppendImportSdkProps(this ProjectRootElement source)
        {
            var sdkProps = source.CreateImportElement("Sdk.props");
            sdkProps.Sdk = MicrosoftNETSdk;
            source.AppendChild(sdkProps);
            return sdkProps;
        }

        private static ProjectImportElement AppendImportSdkTargets(this ProjectRootElement source)
        {
            var sdkProps = source.CreateImportElement("Sdk.targets");
            sdkProps.Sdk = MicrosoftNETSdk;
            source.AppendChild(sdkProps);
            return sdkProps;
        }
    }
}
