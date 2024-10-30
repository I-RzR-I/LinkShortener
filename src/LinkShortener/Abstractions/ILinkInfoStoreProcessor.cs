// ***********************************************************************
//  Assembly         : RzR.Shared.Services.LinkShortener
//  Author           : RzR
//  Created On       : 2024-10-23 17:57
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-10-23 20:31
// ***********************************************************************
//  <copyright file="ILinkInfoStoreProcessor.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using AggregatedGenericResultMessage.Abstractions;

#endregion

namespace LinkShortener.Abstractions
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Interface for link/Url information store processor.
    /// </summary>
    /// =================================================================================================
    public interface ILinkInfoStoreProcessor
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Gets URL by code.
        /// </summary>
        /// <param name="urlKey">The URL key.</param>
        /// <returns>
        ///     The URL by code.
        /// </returns>
        /// =================================================================================================
        IResult<string> GetUrlByCode(string urlKey);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Is unique.
        /// </summary>
        /// <param name="urlKey">The URL key.</param>
        /// <returns>
        ///     An IResult&lt;bool&gt;
        /// </returns>
        /// =================================================================================================
        IResult<bool> IsUnique(string urlKey);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Exists any record.
        /// </summary>
        /// <param name="urlKey">The URL key.</param>
        /// <param name="url">URL of the resource.</param>
        /// <returns>
        ///     An IResult&lt;bool&gt;
        /// </returns>
        /// =================================================================================================
        IResult<bool> ExistsAny(string urlKey, string url);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Exists available record.
        /// </summary>
        /// <param name="urlKey">The URL key.</param>
        /// <param name="url">URL of the resource.</param>
        /// <returns>
        ///     An IResult&lt;bool&gt;
        /// </returns>
        /// =================================================================================================
        IResult<bool> ExistsAvailable(string urlKey, string url);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Disables the URL.
        /// </summary>
        /// <param name="urlKey">The URL key.</param>
        /// <returns>
        ///     An IResult.
        /// </returns>
        /// =================================================================================================
        IResult DisableUrl(string urlKey);

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Generates an URL key.
        /// </summary>
        /// <param name="url">URL of the resource.</param>
        /// <param name="keyLength">(Optional) Length of the key. Default length = 7.</param>
        /// <returns>
        ///     The URL key.
        /// </returns>
        /// =================================================================================================
        IResult<string> GenerateUrlKey(string url, int keyLength = 7);
    }
}