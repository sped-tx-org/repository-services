// -----------------------------------------------------------------------
// <copyright file="RepositoryServices.cs" company="sped-tx.net">
//     Copyright © 2021 sped-tx.net. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Repository.Services
{
    /// <summary>
    /// Defines the <see cref="RepositoryServices" />.
    /// </summary>
    public static class RepositoryServices
    {
        public static IServiceProvider DefaultProvider =>
            DefaultCollection.BuildServiceProvider();

        public static IServiceCollection DefaultCollection
        {
            get
            {
                IServiceCollection services = new ServiceCollection();
                services = services.AddSingleton<IRepositoryOpener, RepositoryOpener>();
                services = services.AddSingleton<IRepositoryDownloader, RepositoryDownloader>();
                services = services.AddSingleton<IRepositoryExpander, RepositoryExpander>();
                services = services.AddSingleton<IRepositoryFileSystem, RepositoryFileSystem>();
                services = services.AddSingleton<RepositoryDependencies>();
                services = services.AddSingleton<IRepositoryGenerator, RepositoryGenerator>();
                return services;
            }
        }

        /// <summary>
        /// The GetService.
        /// </summary>
        /// <typeparam name="TService">.</typeparam>
        /// <returns>The <see cref="TService"/>.</returns>
        public static TService GetService<TService>() where TService: class
        {
            var retVal = DefaultProvider.GetService<TService>();
            return retVal;
        }
    }


    
}
