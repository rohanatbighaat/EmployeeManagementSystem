using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Dto
{
    public class EmployeeDto
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
