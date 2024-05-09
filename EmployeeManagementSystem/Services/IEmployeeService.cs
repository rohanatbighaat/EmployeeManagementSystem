using EmployeeManagementSystem.Model;

namespace EmployeeManagementSystem.Services
{
    public interface IEmployeeService
    {
        public Task<ApiResponse> InsertRecord(InsertRecordRequest request);
        public Task<ApiResponse> GetAllRecord();
        public Task<ApiResponse> GetRecordById(string ID);
        public Task<ApiResponse> UpdateRecordById(string ID, int salary);
        public Task<ApiResponse> DeleteRecordById(string ID);
    }
}
