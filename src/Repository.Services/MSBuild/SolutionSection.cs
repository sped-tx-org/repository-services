using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Build.Construction;

namespace Repository.Services.MSBuild
{


    public class SolutionSection
    {
        public SolutionSection(string type, string name, string state, Dictionary<string, string> values)
        {
            Type = type;
            Name = name;
            State = state;
            Values = values;
        }

        public string Type { get; }

        public string Name { get; }

        public string State { get; }

        public Dictionary<string, string> Values { get; }


        public void WriteTo(TextWriter writer)
        {
            writer.WriteLine($"\t{Type}Section({Name}) = {State}");
            foreach(var kvp in Values)
            {
                writer.WriteLine($"\t\t{kvp.Key} = {kvp.Value}");
            }


            writer.WriteLine($"\tEnd{Type}Section");
        }
    }
}
