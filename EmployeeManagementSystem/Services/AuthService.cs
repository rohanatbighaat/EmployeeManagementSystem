using EmployeeManagementSystem.Helpers;
using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagementSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppSettings _appsettings;
        private readonly ScnEncoder _scnEncoder;
        private readonly ICrudOperationDL _crudOperationDL;

        public AuthService(IOptions<AppSettings> appSettings, ScnEncoder scnEncoder, ICrudOperationDL crudOperationDL)
        {
            _appsettings = appSettings.Value;
            _scnEncoder = scnEncoder;
            _crudOperationDL = crudOperationDL;

        }
        /**
         * Method that initiates the generation of auth token
         **/
        public async Task<AuthResponse?> Authenticate(AuthRequest request)
        {
            request.Scn= _scnEncoder.EncodeScn(request.Scn);
            var user = await _crudOperationDL.GetRecordByNameAndScn(request.Username, request.Scn);
            if (user == null)
            {
                return null;
            }
            var token = await generateJwtToken(user);
            return new AuthResponse(user, token);

        }

        /**
         * Method that generates the jwt auth token
         **/
        private async Task<string> generateJwtToken(InsertRecordRequest user)
        {
            var tokenHandler= new JwtSecurityTokenHandler();
            var token = await Task.Run(() =>
            {
                var key = Encoding.ASCII.GetBytes(_appsettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id) }),
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                return tokenHandler.CreateToken(tokenDescriptor);
            });
            return tokenHandler.WriteToken(token);  
        }

    }
}
