// ***********************************************************************
//  Assembly         : RzR.Shared.Services.LinkShortener
//  Author           : RzR
//  Created On       : 2024-10-21 21:54
// 
//  Last Modified By : RzR
//  Last Modified On : 2024-10-23 20:00
// ***********************************************************************
//  <copyright file="DbInfoHelper.cs" company="">
//   Copyright (c) RzR. All rights reserved.
//  </copyright>
// 
//  <summary>
//  </summary>
// ***********************************************************************

#region U S A G E S

using System.Data;
using System.Data.Common;

#endregion

namespace LinkShortener.Helpers
{
    /// -------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A database information helper.
    /// </summary>
    /// =================================================================================================
    internal static class DbInfoHelper
    {
        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) name of the store link information table.
        /// </summary>
        /// =================================================================================================
        internal const string StoreLinkInfoTableName = "StoreLinkInfo";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     (Immutable) the store link information table script.
        /// </summary>
        /// =================================================================================================
        private static readonly string StoreLinkInfoTableScript = @$"
             CREATE TABLE {StoreLinkInfoTableName}(
                Code varchar(255) NOT NULL,
                Url nvarchar(1024) NOT NULL,
                CreatedClientIp varchar(32) NOT NULL,
                CreatedAtUtc datetime2 NOT NULL,
                ModifiedClientIp varchar(32) NULL,
                ModifiedAtUtc datetime2 NULL,
                IsAvailable bit NOT NULL,
                    CONSTRAINT PK_Code PRIMARY KEY (Code)
             );";

        /// -------------------------------------------------------------------------------------------------
        /// <summary>
        ///     Creates a table.
        /// </summary>
        /// <param name="dbConnection">The database connection.</param>
        /// =================================================================================================
        internal static void CreateTable(DbConnection dbConnection)
        {
            try
            {
                var script = StoreLinkInfoTableScript;
                var dbType = dbConnection.GetType();
                if (dbType.Name.ToLower().Contains("npgsql"))
                {
                    script = script.Replace("nvarchar", "varchar")
                        .Replace("datetime2", "timestamp")
                        .Replace("datetime", "timestamp")
                        .Replace("bit", "boolean");
                }

                if (dbType.Name.ToLower().Contains("mysql"))
                {
                    script = script.Replace("nvarchar", "varchar")
                        .Replace("datetime2", "timestamp")
                        .Replace("datetime", "timestamp")
                        .Replace("bit", "boolean");
                }

                if (dbConnection.State != ConnectionState.Open)
                    dbConnection.Open();

                using var cmd = dbConnection.CreateCommand();
                cmd.CommandText = script;
                cmd.ExecuteNonQuery();
                dbConnection.Close();
            }
            catch
            {
                // ignored
            }
            finally
            {
                if (dbConnection.State != ConnectionState.Closed)
                    dbConnection.Close();
            }
        }
    }
}