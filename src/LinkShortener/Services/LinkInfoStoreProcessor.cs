// ***********************************************************************
//  Assembly         : RzR.Shared.Services.LinkShortener
//  Author           : RzR
//  Created On       : 2024-10-23 17:57
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-10-23 17:57
// ***********************************************************************
//  <copyright file="LinkInfoStoreProcessor.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

using AggregatedGenericResultMessage;
using AggregatedGenericResultMessage.Abstractions;
using AggregatedGenericResultMessage.Extensions.Result;
using DomainCommonExtensions.CommonExtensions.TypeParam;
using LinkShortener.Abstractions;
using LinkShortener.Helpers;
using System.Data;
using System;
using DomainCommonExtensions.DataTypeExtensions;
// ReSharper disable ClassNeverInstantiated.Global

namespace LinkShortener.Services
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A link information store processor.
    /// </summary>
    /// <seealso cref="T:LinkShortener.Abstractions.ILinkInfoStoreProcessor"/>
    ///
    /// ### <inheritdoc cref="ILinkInfoStoreProcessor"/>
    /// =================================================================================================
    public class LinkInfoStoreProcessor : ILinkInfoStoreProcessor
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the database connection.
        /// </summary>
        /// =================================================================================================
        private readonly IDbConnection _dbConnection;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the client browser information service.
        /// </summary>
        /// =================================================================================================
        private readonly IClientBrowserInfoService _clientBrowserInfoService;

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Initializes a new instance of the <see cref="LinkInfoStoreProcessor"/> class.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// <param name="clientBrowserInfoService">The client browser information service.</param>
        /// =================================================================================================
        public LinkInfoStoreProcessor(IDbConnection dbConnection,
            IClientBrowserInfoService clientBrowserInfoService)
        {
            _dbConnection = dbConnection;
            _clientBrowserInfoService = clientBrowserInfoService;
        }

        /// <inheritdoc/>
        public IResult<string> GetUrlByCode(string urlKey)
        {
            try
            {
                using var cmd = _dbConnection.CreateCommand();
                cmd.CommandText = @$"SELECT Url FROM {DbInfoHelper.StoreLinkInfoTableName} 
                                WHERE IsAvailable = @ParamIsAvailable AND Code = @ParamUrlKey";

                var isAvailableParam = cmd.CreateParameter();
                isAvailableParam.DbType = DbType.Boolean;
                isAvailableParam.ParameterName = "ParamIsAvailable";
                isAvailableParam.Value = true;
                cmd.Parameters.Add(isAvailableParam);

                var urlKeyParam = cmd.CreateParameter();
                urlKeyParam.DbType = DbType.AnsiString;
                urlKeyParam.ParameterName = "ParamUrlKey";
                urlKeyParam.Value = urlKey;
                cmd.Parameters.Add(urlKeyParam);

                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();

                var data = cmd.ExecuteScalar();
                _dbConnection.Close();

                return Result<string>.Success((string)data);
            }
            catch (Exception e)
            {
                return Result<string>
                    .Failure(e.Message)
                    .WithError(e);
            }
            finally
            {
                if (_dbConnection.State != ConnectionState.Closed)
                    _dbConnection.Close();
            }
        }

        /// <inheritdoc/>
        public IResult<bool> IsUnique(string urlKey)
        {
            try
            {
                using var cmd = _dbConnection.CreateCommand();
                cmd.CommandText = @$"SELECT COUNT(Code) AS COUNT FROM {DbInfoHelper.StoreLinkInfoTableName} 
                                WHERE Code = @ParamUrlKey";

                var urlKeyParam = cmd.CreateParameter();
                urlKeyParam.DbType = DbType.AnsiString;
                urlKeyParam.ParameterName = "ParamUrlKey";
                urlKeyParam.Value = urlKey;
                cmd.Parameters.Add(urlKeyParam);

                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();

                var data = cmd.ExecuteScalar();
                _dbConnection.Close();

                return Result<bool>.Success(Convert.ToInt64(data) <= 0);
            }
            catch (Exception e)
            {
                return Result<bool>
                    .Failure(e.Message)
                    .WithError(e);
            }
            finally
            {
                if (_dbConnection.State != ConnectionState.Closed)
                    _dbConnection.Close();
            }
        }

        /// <inheritdoc/>
        public IResult<bool> ExistsAny(string urlKey, string url)
        {
            try
            {
                using var cmd = _dbConnection.CreateCommand();
                cmd.CommandText = @$"SELECT COUNT(Code) AS COUNT FROM {DbInfoHelper.StoreLinkInfoTableName} 
                                WHERE Code = @ParamUrlKey AND Url = @ParamUrl";

                var urlKeyParam = cmd.CreateParameter();
                urlKeyParam.DbType = DbType.AnsiString;
                urlKeyParam.ParameterName = "ParamUrlKey";
                urlKeyParam.Value = urlKey;
                cmd.Parameters.Add(urlKeyParam);

                var urlParam = cmd.CreateParameter();
                urlParam.DbType = DbType.AnsiString;
                urlParam.ParameterName = "ParamUrl";
                urlParam.Value = url;
                cmd.Parameters.Add(urlParam);

                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();

                var data = cmd.ExecuteScalar();
                _dbConnection.Close();

                return Result<bool>.Success(Convert.ToInt64(data) > 0);
            }
            catch (Exception e)
            {
                return Result<bool>
                    .Failure(e.Message)
                    .WithError(e);
            }
            finally
            {
                if (_dbConnection.State != ConnectionState.Closed)
                    _dbConnection.Close();
            }
        }

        /// <inheritdoc/>
        public IResult<bool> ExistsAvailable(string urlKey, string url)
        {
            try
            {
                using var cmd = _dbConnection.CreateCommand();
                cmd.CommandText = @$"SELECT COUNT(Code) FROM {DbInfoHelper.StoreLinkInfoTableName} 
                                WHERE Code = @ParamUrlKey AND Url = @ParamUrl AND IsAvailable = @ParamIsAvailable";

                var urlKeyParam = cmd.CreateParameter();
                urlKeyParam.DbType = DbType.AnsiString;
                urlKeyParam.ParameterName = "ParamUrlKey";
                urlKeyParam.Value = urlKey;
                cmd.Parameters.Add(urlKeyParam);

                var urlParam = cmd.CreateParameter();
                urlParam.DbType = DbType.AnsiString;
                urlParam.ParameterName = "ParamUrl";
                urlParam.Value = url;
                cmd.Parameters.Add(urlParam);

                var isAvailableParam = cmd.CreateParameter();
                isAvailableParam.DbType = DbType.Boolean;
                isAvailableParam.ParameterName = "ParamIsAvailable";
                isAvailableParam.Value = true;
                cmd.Parameters.Add(isAvailableParam);

                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();

                var data = cmd.ExecuteScalar();
                _dbConnection.Close();

                return Result<bool>.Success(Convert.ToInt64(data) > 0);
            }
            catch (Exception e)
            {
                return Result<bool>
                    .Failure(e.Message)
                    .WithError(e);
            }
            finally
            {
                if (_dbConnection.State != ConnectionState.Closed)
                    _dbConnection.Close();
            }
        }

        /// <inheritdoc/>
        public IResult DisableUrl(string urlKey)
        {
            try
            {
                using var cmd = _dbConnection.CreateCommand();
                cmd.CommandText = @$"UPDATE {DbInfoHelper.StoreLinkInfoTableName}
                                SET
                                    ModifiedClientIp = @ParamModifiedClientIp,
                                    ModifiedAtUtc = @ParamModifiedAtUtc,
                                    IsAvailable = @ParamIsAvailable
                                WHERE
                                    Code = @ParamUrlKey";

                var clientIpParam = cmd.CreateParameter();
                clientIpParam.DbType = DbType.AnsiString;
                clientIpParam.ParameterName = "ParamModifiedClientIp";
                clientIpParam.Value = _clientBrowserInfoService.GetClientIp().IfIsNull("unknown");
                cmd.Parameters.Add(clientIpParam);

                var createdAtParam = cmd.CreateParameter();
                //createdAtParam.DbType = DbType.DateTime2;
                createdAtParam.ParameterName = "ParamModifiedAtUtc";
                createdAtParam.Value = DateTime.Now.AsUtc();
                cmd.Parameters.Add(createdAtParam);

                var isAvailableParam = cmd.CreateParameter();
                isAvailableParam.DbType = DbType.Boolean;
                isAvailableParam.ParameterName = "ParamIsAvailable";
                isAvailableParam.Value = false;
                cmd.Parameters.Add(isAvailableParam);

                var urlKeyParam = cmd.CreateParameter();
                urlKeyParam.DbType = DbType.AnsiString;
                urlKeyParam.ParameterName = "ParamUrlKey";
                urlKeyParam.Value = urlKey;
                cmd.Parameters.Add(urlKeyParam);

                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();

                cmd.ExecuteNonQuery();

                return Result.Success();
            }
            catch (Exception e)
            {
                return Result
                    .Failure(e.Message)
                    .WithError(e);
            }
            finally
            {
                if (_dbConnection.State != ConnectionState.Closed)
                    _dbConnection.Close();
            }
        }

        /// <inheritdoc />
        public IResult<string> GenerateUrlKey(string url, int keyLength = 7)
        {
            try
            {
                string urlKey;
                do
                {
                    urlKey = CodeGeneratorHelper.GenerateUrlCode(keyLength);
                } while (IsUnique(urlKey).Response.IsFalse());

                var savedUrlKey = AddNewUrl(urlKey, url);

                if (savedUrlKey.IsSuccess.IsFalse())
                    return Result<string>.Failure(savedUrlKey.GetFirstMessage());

                return Result<string>.Success(urlKey);
            }
            catch (Exception e)
            {
                return Result<string>
                    .Failure(e.Message)
                    .WithError(e);
            }
            finally
            {
                if (_dbConnection.State != ConnectionState.Closed)
                    _dbConnection.Close();
            }
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Adds a new URL to store.
        /// </summary>
        /// <param name="urlKey">The URL key.</param>
        /// <param name="url">URL of the resource.</param>
        /// <returns>
        ///     An IResult.
        /// </returns>
        /// =================================================================================================
        private IResult AddNewUrl(string urlKey, string url)
        {
            try
            {
                using var cmd = _dbConnection.CreateCommand();
                cmd.CommandText = @$"INSERT INTO {DbInfoHelper.StoreLinkInfoTableName}
                                (Code,Url,CreatedClientIp,CreatedAtUtc,IsAvailable)
                                VALUES(@ParamUrlKey, @ParamUrl, @ParamCreatedClientIp, @ParamCreatedAt, @ParamIsAvailable)";

                var urlKeyParam = cmd.CreateParameter();
                urlKeyParam.DbType = DbType.AnsiString;
                urlKeyParam.ParameterName = "ParamUrlKey";
                urlKeyParam.Value = urlKey;
                cmd.Parameters.Add(urlKeyParam);

                var urlParam = cmd.CreateParameter();
                urlParam.DbType = DbType.AnsiString;
                urlParam.ParameterName = "ParamUrl";
                urlParam.Value = url;
                cmd.Parameters.Add(urlParam);

                var clientIpParam = cmd.CreateParameter();
                clientIpParam.DbType = DbType.AnsiString;
                clientIpParam.ParameterName = "ParamCreatedClientIp";
                clientIpParam.Value = _clientBrowserInfoService.GetClientIp().IfIsNull("unknown");
                cmd.Parameters.Add(clientIpParam);

                var createdAtParam = cmd.CreateParameter();
                //createdAtParam.DbType = DbType.DateTime2;
                createdAtParam.ParameterName = "ParamCreatedAt";
                createdAtParam.Value = DateTime.Now.AsUtc();
                cmd.Parameters.Add(createdAtParam);

                var isAvailableParam = cmd.CreateParameter();
                isAvailableParam.DbType = DbType.Boolean;
                isAvailableParam.ParameterName = "ParamIsAvailable";
                isAvailableParam.Value = true;
                cmd.Parameters.Add(isAvailableParam);

                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();

                cmd.ExecuteNonQuery();

                return Result.Success();
            }
            catch (Exception e)
            {
                return Result
                    .Failure(e.Message)
                    .WithError(e);
            }
            finally
            {
                if (_dbConnection.State != ConnectionState.Closed)
                    _dbConnection.Close();
            }
        }
    }
}