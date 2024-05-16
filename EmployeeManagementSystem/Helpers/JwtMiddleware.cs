using EmployeeManagementSystem.Model;
using EmployeeManagementSystem.Repository;
using EmployeeManagementSystem.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EmployeeManagementSystem.Helpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private readonly ICrudOperationDL _crudOperationDL;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings, ICrudOperationDL crudOperationDL)
        {
            _next = next;
            _appSettings = appSettings.Value;
            _crudOperationDL = crudOperationDL;

        }
        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await attachUserToContext(context, token);

            await _next(context);
        }

        private async Task attachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;

                //Attach user to context on successful JWT validation
                context.Items["User"] = await _crudOperationDL.GetRecordById(userId);
            }
            catch(Exception ex)
            {
                // user is not attached to context so the request won't have access to secure routes
                throw new Exception("JWT Authentication Failed:  " + ex.Message);
            }
        }
    }
}
