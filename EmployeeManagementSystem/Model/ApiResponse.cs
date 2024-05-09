using EmployeeManagementSystem.Dto;

namespace EmployeeManagementSystem.Model
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<EmployeeDto> data { get; set; }

    }
}
