namespace EmployeeManagementSystem.Model
{
    public class AuthResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }

        public AuthResponse(InsertRecordRequest employee, string token)
        {
            Id = employee.Id;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Token = token;
        }
    }
}
