namespace EmployeeManagementSystem.Model
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public List<InsertRecordRequest> dataList { get; set; }

        public InsertRecordRequest data { get; set; }
    }
}
