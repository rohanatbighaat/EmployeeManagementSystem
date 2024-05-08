using EmployeeManagementSystem.Model;
using System.Globalization;

namespace EmployeeManagementSystem.Repository
{
    public interface ICrudOperationDL
    {
        public Task<ApiResponse> InsertRecord(InsertRecordRequest request);
        public Task<ApiResponse> GetAllRecord();
        public Task<ApiResponse> GetRecordById(string ID);
        public Task<ApiResponse> UpdateRecordById(string ID, int salary);
        public Task<ApiResponse> DeleteRecordById(string ID);
    }
}
