using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Client.Services;

public class BaseService
{
    protected string ConnectionString { get; private set; }

    public BaseService()
    {
        // läser appsettings.json i mappen där programmet körs
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // hämtar anslutningssträngen som heter pgsql
        ConnectionString = config.GetConnectionString("pgsql")
            ?? throw new InvalidOperationException("Connection string 'pgsql' not found.");
    }
}
