using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Build.Construction;

namespace Repository.Services.MSBuild
{
    internal static class ProjectFileFactory
    {
        private const string LibraryOutputType = "Library";
        private const string MicrosoftNETSdk = "Microsoft.NET.Sdk";
        public static ProjectRootElement Create(IRepositoryProject project)
        {
            ProjectRootElement root = ProjectRootElement.Create();

            var group0 = root.CreatePropertyGroupElement();
            root.AppendChild(group0);
            group0.AddProperty("IsPackable", "false");

            var sdkProps = root.CreateImportElement("Sdk.props");
            sdkProps.Sdk = MicrosoftNETSdk;
            root.AppendChild(sdkProps);

            var group1 = root.CreatePropertyGroupElement();
            root.AppendChild(group1);
            group1.AddProperty("TargetFramework", project.TargetFramework);
            group1.AddProperty("RootNamespace", project.RootNamespace);
            if (project.OutputType != LibraryOutputType)
            {
                group1.AddProperty("OutputType", project.OutputType);
            }

            var sdkTargets = root.CreateImportElement("Sdk.targets");
            sdkTargets.Sdk = MicrosoftNETSdk;
            root.AppendChild(sdkTargets);

            return root;
        }
    }
}
