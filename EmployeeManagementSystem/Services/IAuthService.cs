using EmployeeManagementSystem.Model;

namespace EmployeeManagementSystem.Services
{
    public interface IAuthService
    {
        Task<AuthResponse?> Authenticate(AuthRequest request);
    }
}
