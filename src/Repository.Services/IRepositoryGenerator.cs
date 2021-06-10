using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Services
{
    public interface IRepositoryGenerator
    {
        Task CreateRepositoryAsync(IRepositorySettings settings, CancellationToken cancellationToken = default);
    }
}
