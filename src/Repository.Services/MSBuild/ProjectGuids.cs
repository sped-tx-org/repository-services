using System;

namespace Repository.Services.MSBuild
{
    internal static class ProjectGuids
    {
        public const string CSharpString = "{9A19103F-16F7-4668-BE54-9A1E7A4F7556}";
        public static Guid CSharpGuid = Guid.Parse(CSharpString);

        public const string SolutionFolderString = "{2150E333-8FDC-42A3-9474-1A3956D46DE8}";
        public static Guid SolutionFolderGuid = Guid.Parse(SolutionFolderString);
    }
}
