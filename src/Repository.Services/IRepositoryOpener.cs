﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services
{

    public interface IRepositoryOpener
    {
        Task CollapseSolutionAsync();
        Task OpenRepositoryAsync(string solutionFilePath);
    }
}
