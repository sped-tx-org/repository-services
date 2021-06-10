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
        /// <summary>
        /// Gets the DefaultCollection.
        /// </summary>
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
        /// Gets the DefaultProvider.
        /// </summary>
        public static IServiceProvider DefaultProvider =>
            DefaultCollection.BuildServiceProvider();

        /// <summary>
        /// Gets a service of type <typeparamref name="TService"/>. The <typeparamref name="TService"/> must be an interface.
        /// </summary>
        /// <typeparam name="TService">The type of service to get</typeparam>
        /// <returns>The <typeparamref name="TService"/> or <c>null</c></returns>
        public static TService GetService<TService>() where TService : class
        {
            var retVal = DefaultProvider.GetService<TService>();
            return retVal;
        }
    }
}
