// -----------------------------------------------------------------------
// <copyright file="ProjectFileFactory.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

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
