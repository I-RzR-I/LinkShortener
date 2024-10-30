From the beginning, you must add a few rows in the `Startup.cs` class: 

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // ...
    
    services.RegisterLinkShortenerService();
        
    // ...
}
```

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // ...
    
    var connection = new SqlConnection(dbConnectionString);
    services.RegisterLinkShortenerService(connection);
        
    // ...
}
```

As in the repository all queries were written `ADO.NET` and the data source connection in the `RegisterLinkShortenerService` is a type of `DbConnection`, you can provide any type. There was tested with: `MS SqlServer`, `SqlLite`, `Npgsql`, `MySql`.

In current implementation are available 2 service:

- `IClientBrowserInfoService` - allow to see some info about the current clinet.
- `ILinkInfoStoreProcessor` - provides methods that allows URL manipulations.

After this step you can use available methdos from `ILinkInfoStoreProcessor`. 

- `GetUrlByCode(string urlKey)` -> Get URL hidden by supplied code;
- `IsUnique(string urlKey)` -> Check if the supplied code is unique or not
- `ExistsAny(string urlKey, string url)` -> Check if the supplied code and URL exist in the data source;
- `ExistsAvailable(string urlKey, string url)` - Check if the supplied code and URL exist in the data source and it is available (not disabled);
- `DisableUrl(string urlKey)` - Disable/inactivate supplied URL code;
- `GenerateUrlKey(string url, int keyLength = 7)` - Generate new URL code and store it in the data source.