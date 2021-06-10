using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{

    internal class RepositoryOpener : IRepositoryOpener
    {
        public async Task CollapseSolutionAsync()
        {
            await Task.CompletedTask;
        }

        public async Task OpenRepositoryAsync(string solutionFilePath)
        {
            Process.Start(solutionFilePath);
            await Task.CompletedTask;
        }
    }
}
