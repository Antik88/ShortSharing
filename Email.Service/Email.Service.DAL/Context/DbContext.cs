﻿using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Email.Service.DAL.Context;

public class DbContext 
{
    private readonly IConfiguration _configuration; 
    private readonly IMongoDatabase _database;

    public DbContext(IConfiguration configuration)
    {
        _configuration = configuration;

        //var connectionString = _configuration.GetConnectionString("DbConnection");
        var connectionString = Environment.GetEnvironmentVariable("DbConnection");
        var mongoUrl = MongoUrl.Create(connectionString);
        var mongoClient = new MongoClient(mongoUrl);
        _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
    }

    public IMongoDatabase? Database => _database; 
}
