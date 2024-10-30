// ***********************************************************************
//  Assembly         : RzR.Shared.Services.LinkShortener
//  Author           : RzR
//  Created On       : 2024-10-21 19:33
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-10-23 20:38
// ***********************************************************************
//  <copyright file="DependencyInjection.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using LinkShortener.Abstractions;
using LinkShortener.Helpers;
using LinkShortener.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.Common;
using UniqueServiceCollection;

// ReSharper disable UnusedMethodReturnValue.Local

#endregion

namespace LinkShortener
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A dependency injection.
    /// </summary>
    /// =================================================================================================
    public static class DependencyInjection
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     An IServiceCollection extension method that registers the link shortener service.
        /// </summary>
        /// <param name="serviceCollection">The serviceCollection to act on.</param>
        /// <param name="linkStoreDbConnection">The link store database connection.</param>
        /// <returns>
        ///     An IServiceCollection.
        /// </returns>
        /// =================================================================================================
        public static IServiceCollection RegisterLinkShortenerService(
            this IServiceCollection serviceCollection, DbConnection linkStoreDbConnection)
        {
            serviceCollection.AddHttpContextAccessor();

            serviceCollection.AddSingleton<IDbConnection>(_ => linkStoreDbConnection);

            DbInfoHelper.CreateTable(linkStoreDbConnection);

            serviceCollection.AddUnique<IClientBrowserInfoService, ClientBrowserInfoService>();
            serviceCollection.AddUnique<ILinkInfoStoreProcessor, LinkInfoStoreProcessor>();

            return serviceCollection;
        }
    }
}