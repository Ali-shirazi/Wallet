using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Wallet.Api.Auth
{
    public class Auth : IJwtAuth
        {
            private readonly string _username = RConfigurationManager.AppSetting["JWT:user"];
            private readonly string _password = RConfigurationManager.AppSetting["JWT:pass"];
            private readonly string key;
            public Auth(string key)
            {
                this.key = key;
            }
            public string Authentication(string username, string password, string sysId, Guid userId)
            {
                if (!(username.Equals(username) || password.Equals(password)))
                {
                    return null;
                }

                // 1. Create Security Token Handler
                var tokenHandler = new JwtSecurityTokenHandler();

                // 2. Create Private Key to Encrypted
                var tokenKey = Encoding.ASCII.GetBytes(key);

                //3. Create JETdescriptor
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                        new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.System, sysId)
                        }),
                    Expires = DateTime.UtcNow.AddHours(5),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.Aes128CbcHmacSha256)
                };
                //4. Create Token
                var token = tokenHandler.CreateToken(tokenDescriptor);

                // 5. Return Token from method
                return tokenHandler.WriteToken(token);
            }
        }
    }


