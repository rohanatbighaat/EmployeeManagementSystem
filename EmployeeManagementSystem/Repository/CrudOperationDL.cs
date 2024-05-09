using EmployeeManagementSystem.Exceptions;
using EmployeeManagementSystem.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Reflection.Metadata.Ecma335;

namespace EmployeeManagementSystem.Repository
{
    public class CrudOperationDL : ICrudOperationDL
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<InsertRecordRequest> _mongoCollection; 

        public CrudOperationDL(IConfiguration configuration)
        {
            _configuration = configuration;  // picks stuff from appsetting.json
            _mongoClient = new MongoClient(_configuration["DatabaseSettings:ConnectionString"]);
            var _MongoDatabase = _mongoClient.GetDatabase(_configuration["DatabaseSettings:DatabaseName"]);
            _mongoCollection = _MongoDatabase.GetCollection<InsertRecordRequest>(_configuration["DatabaseSettings:CollectionName"]);

        }

        public async Task<bool> DeleteRecordById(string ID)
        {  
                var Result = await _mongoCollection.DeleteOneAsync(x => (x.Id == ID));
                return Result.IsAcknowledged;

        }

        public async Task<List<InsertRecordRequest>> GetAllRecord()
        {
                return await _mongoCollection.Find(x => true).ToListAsync(); ;
        }

        public async Task<InsertRecordRequest> GetRecordById(string ID)
        {
            
                return await _mongoCollection.Find(x => (x.Id == ID)).FirstOrDefaultAsync();
                
        }

        public async Task InsertRecord(InsertRecordRequest request)
        {
                await _mongoCollection.InsertOneAsync(request); 
        }

        public async Task<bool> UpdateRecordById(string ID, int salary)
        {
            
                var Filter = new BsonDocument().Add("Salary", salary).Add("UpdatedAt", DateTime.Now.ToString());
                var UpdateQuery = new BsonDocument("$set",Filter);
                var Result= await _mongoCollection.UpdateManyAsync(x => (x.Id == ID), UpdateQuery);
                return Result.IsAcknowledged;    
        }

        public async Task<InsertRecordRequest> GetRecordByNameAndScn(string name, string scn)
        {
            return await _mongoCollection.Find(x => (x.FirstName == name && x.Scn== scn)).FirstOrDefaultAsync();
        }
    }
}
