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

        public async Task<ApiResponse> DeleteRecordById(string ID)
        {
            ApiResponse response = new ApiResponse();
            response.Success = true;
            response.Message = "Employee Deleted Successfully";
            try
            {
                var Result = await _mongoCollection.DeleteOneAsync(x => (x.Id == ID));
                if (!Result.IsAcknowledged)
                {
                    response.Message = "Employee Deletion Failed";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Exception occured: "+ ex.Message;
            }
            return response;

        }

        public async Task<ApiResponse> GetAllRecord()
        {
            ApiResponse response = new ApiResponse();
            response.Success = true;
            response.Message = "Data Extraction successful";

            try
            {
                response.dataList = new List<InsertRecordRequest>();
                response.dataList = await _mongoCollection.Find(x => true).ToListAsync();
                if (response.dataList.Count ==0)
                {
                    response.Message = "No data to display";
                }

            }catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Exception occured:" + ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse> GetRecordById(string ID)
        {
            ApiResponse response = new ApiResponse();
            response.Success = true;
            response.Message = "Data Extraction successful";

            try
            {
                response.data = new InsertRecordRequest();
                response.data = await _mongoCollection.Find(x => (x.Id == ID)).FirstOrDefaultAsync();
                if (response.data == null)
                {
                    response.Message = "No data to display";
                }

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Exception occured:" + ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse> InsertRecord(InsertRecordRequest request)
        {
            ApiResponse response = new ApiResponse();
            response.Success = true;
            response.Message = "Onboarding successful";
            try
            {
                request.CreatedAt = DateTime.Now.ToString();
                request.UpdatedAt = string.Empty;

                await _mongoCollection.InsertOneAsync(request); 
    
            }catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Exception occured:" +ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse> UpdateRecordById(string ID, int salary)
        {
            ApiResponse response = new ApiResponse();
            response.Success = true;
            response.Message = "Salary updated successfully";

            try
            {
                // one way to update
               /* ApiResponse employee = await GetRecordById(ID);
                employee.data.UpdatedAt = DateTime.Now.ToString();
                employee.data.Salary= salary;
                var Result = await _mongoCollection.ReplaceOneAsync(x => (x.Id == ID), employee.data);
                response.data = employee.data;*/

                var Filter = new BsonDocument().Add("Salary", salary).Add("UpdatedAt", DateTime.Now.ToString());
                var UpdateQuery = new BsonDocument("$set",Filter);
                var Result= await _mongoCollection.UpdateManyAsync(x => (x.Id == ID), UpdateQuery);

                if (!Result.IsAcknowledged)
                {
                    response.Message = "Updation failure";
                }


            }catch(Exception ex)
            {
                response.Success = false;
                response.Message= "Exception occured: "+ ex.Message;
            }
            return response;    
        }
    }
}
