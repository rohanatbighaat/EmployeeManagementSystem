using EmployeeManagementSystem.Model;
using MongoDB.Driver;
using System.Globalization;

namespace EmployeeManagementSystem.Repository
{
    public interface ICrudOperationDL
    {
        public Task InsertRecord(InsertRecordRequest request);
        public Task<List<InsertRecordRequest>> GetAllRecord();
        public Task<InsertRecordRequest> GetRecordById(string ID);
        public Task<bool> UpdateRecordById(string ID, int salary);
        public Task<bool> DeleteRecordById(string ID);
        public Task<InsertRecordRequest> GetRecordByNameAndScn(string name, string scn);
        public Task<bool> DoesPhoneNumberExists(string phoneNumber);
    }
}
