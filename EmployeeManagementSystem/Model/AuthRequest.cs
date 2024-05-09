using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Model
{
    public class AuthRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Scn { get; set; }
    }
}
