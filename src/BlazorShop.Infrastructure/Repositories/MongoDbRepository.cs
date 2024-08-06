using System.Linq.Expressions;
using BlazorShop.Domain.Repositories.Interfaces;
using BlazorShop.Infrastructure.Persistence.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BlazorShop.Infrastructure.Repositories;

public class MongoDbRepository(MongoDbContext mongoDbContext) : IMongoDbRepository
{
    private const string IdColumnName = "_id";
    private readonly IMongoDatabase _mongoDatabase = mongoDbContext.MongoDatabase;

    public async Task<IEnumerable<T>> FindAll<T>(Expression<Func<T, bool>> predicate)
    {
        var collectionName = typeof(T).Name;
        return await _mongoDatabase.GetCollection<T>(collectionName).Find(predicate).ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAll<T>()
    {
        var collectionName = typeof(T).Name;
        return await _mongoDatabase.GetCollection<T>(collectionName).Find(new BsonDocument()).ToListAsync();
    }

    public async Task<T> GetById<T>(Guid id)
    {
        var collectionName = typeof(T).Name;
        var filter = Builders<T>.Filter.Eq(IdColumnName, id.ToString());
        return await _mongoDatabase.GetCollection<T>(collectionName).Find(filter).SingleAsync();
    }

    public async Task<bool> Any<T>()
    {
        var collectionName = typeof(T).Name;
        return await _mongoDatabase.GetCollection<T>(collectionName).Find(new BsonDocument()).AnyAsync();
    }

    public async Task Insert<T>(T entity)
    {
        var collectionName = typeof(T).Name;
        await _mongoDatabase.GetCollection<T>(collectionName).InsertOneAsync(entity);
    }

    public async Task Update<T>(Guid id, T entity)
    {
        var collectionName = typeof(T).Name;
        var filter = Builders<T>.Filter.Eq(IdColumnName, id.ToString());
        await _mongoDatabase.GetCollection<T>(collectionName).ReplaceOneAsync(filter, entity);
    }

    public async Task Delete<T>(Guid id)
    {
        var collectionName = typeof(T).Name;
        var filter = Builders<T>.Filter.Eq(IdColumnName, id.ToString());
        await _mongoDatabase.GetCollection<T>(collectionName).DeleteOneAsync(filter);
    }
}