// -----------------------------------------------------------------------
// <copyright file="SolutionSection.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;

namespace Repository.Services.MSBuild
{
    /// <summary>
    /// Defines the <see cref="SolutionSection" />.
    /// </summary>
    public class SolutionSection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionSection"/> class.
        /// </summary>
        /// <param name="type">The type<see cref="string"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="state">The state<see cref="string"/>.</param>
        /// <param name="values">The values<see cref="Dictionary{string, string}"/>.</param>
        public SolutionSection(string type, string name, string state, Dictionary<string, string> values)
        {
            Type = type;
            Name = name;
            State = state;
            Values = values;
        }

        /// <summary>
        /// Gets the Name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the State.
        /// </summary>
        public string State { get; }

        /// <summary>
        /// Gets the Type.
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Gets the Values.
        /// </summary>
        public Dictionary<string, string> Values { get; }

        /// <summary>
        /// The WriteTo.
        /// </summary>
        /// <param name="writer">The writer<see cref="TextWriter"/>.</param>
        public void WriteTo(TextWriter writer)
        {
            writer.WriteLine($"\t{Type}Section({Name}) = {State}");
            foreach (var kvp in Values)
            {
                writer.WriteLine($"\t\t{kvp.Key} = {kvp.Value}");
            }


            writer.WriteLine($"\tEnd{Type}Section");
        }
    }
}
