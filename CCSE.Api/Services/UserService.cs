using CCSE.Api.Domain;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace CCSE.Api.Services
{
    public class UserService
    {
        //string cs = "Host=localhost;Username=postgres;Password=s$cret;Database=testdb";
        string cs = "Server=http://database-1.cfv10rew3vrj.us-east-1.rds.amazonaws.com/;Port=5432;User Id=postgres;Password=isuruCcamsciit;Database=CC_CSE";
        string secret = "hdsgdsg@hifdfu76%^jkfhdkjfhdgdj@121908kjhfd";
        public bool ValidateLogin(string token)
        {
            try
            {
                return true;

            }
            catch (Exception ex)
            {
                return false;
               
            }




        }



        public string UserLogin(User user)
        {
            string token = "";

            using var con = new NpgsqlConnection(cs);
            con.Open();

            string sql = "SELECT * FROM [User] Where UserName=";
            using var cmd = new NpgsqlCommand(sql, con);

            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            
            if (rdr.HasRows)
            {
                token = GenerateJwtToken(user);
            }
            else
            {
                token = "";
            }
            return token;
        }

        public string GenerateJwtToken(User user)
        {
            var expiresDate = DateTime.UtcNow.AddDays(2);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("UserName",user.UserName),
                }),
                Expires = expiresDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private bool ValidateFormAuthenticationToken( string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var tokenUserName = jwtToken.Claims.First(x => x.Type == "UserName").Value;
            var tokenUserId = int.Parse(jwtToken.Claims.First(x => x.Type == "UserId").Value);
            //if (tokenUserId > 0 )
            //{
            //    var activeUser = await _userService.GetActiveUser(tokenUserId);
            //    if (activeUser != null)
            //    {
            //        if (activeUser.UserName == email)
            //        {
            //            return true;
            //        }
            //    }
            //}

            return false;
        }
    }
}
