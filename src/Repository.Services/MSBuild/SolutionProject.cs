using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Build.Construction;

namespace Repository.Services.MSBuild
{

    public class SolutionProject
    {
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

        public Guid ProjectTypeGuid { get; }

        public string ProjectName { get; }

        public string RelativePath { get; }

        public Guid ProjectGuid { get; }

        public SolutionSection SolutionItems { get; }

        internal string ProjectGuidString => ProjectGuid.ToString("B").ToUpper();

        internal string ProjectTypeGuidString => ProjectTypeGuid.ToString("B").ToUpper();

        public bool HasSolutionItems =>
            SolutionItems?.Values.Count > 0;

        public void WriteTo(TextWriter writer)
        {
            var projectTypeGuid = ProjectTypeGuid.ToString("B").ToUpper();
            var projectGuid = ProjectGuid.ToString("B").ToUpper();
            writer.WriteLine($"Project(\"{projectTypeGuid}\") = \"{ProjectName}\", \"{RelativePath}\", \"{projectGuid}\"");
            if (HasSolutionItems)
            {
                writer.WriteLine($"\tProjectSection({SolutionItems.Name}) = {SolutionItems.State}");
                foreach(var kvp in SolutionItems.Values)
                {
                    writer.WriteLine($"\t\t{kvp.Key} = {kvp.Value}");
                }

                writer.WriteLine("\tEndProjectSection");

            }

            writer.WriteLine("EndProject");
        }
    }
}
