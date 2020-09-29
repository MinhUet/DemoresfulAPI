using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Nhom9.Docker_Kubernet.Demo.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nhom9.Docker_Kubernet.Demo.DL.Database
{
    public class MongoDatabaseContext : IDatabaseContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoClient _mongoClient;
        private readonly ILogger<MongoDatabaseContext> _logger;

        public MongoDatabaseContext(ILogger<MongoDatabaseContext> logger, IDatabaseSettings settings)
        {
            _logger = logger;
            try
            {
                _mongoClient = new MongoClient(settings.ConnectionString);
                _mongoDatabase = _mongoClient.GetDatabase(settings.DatabaseName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"\nError while connecting to MongoDB: {ex}\n");
            }
        }

        public IMongoDatabase MongoDatabase { get => _mongoDatabase; }
    }
}
