using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventsManagement.Helpers
{
    public class GenerateKeys(IConfiguration _Configuration)
    {
        private static readonly Random _random = new Random();
        public static string GenerateId(int length = 16)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }


        public static string GenerateNumberId(int length)
        {
            const string chars = "0123456789";
            var r = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
            return r;
        }


        public string CreateStudentToken(int Id)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,Id.ToString()),
                new Claim(ClaimTypes.Role,"Student")
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_Configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _Configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _Configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1000),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }


        public string CreateEmployeeToken(int Id)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,Id.ToString()),
                new Claim(ClaimTypes.Role,"Employee")
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_Configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _Configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _Configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1000),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

    }
}
