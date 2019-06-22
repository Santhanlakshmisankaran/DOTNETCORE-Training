using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trainingmiddleware.Interface;
using trainingmiddleware.Model;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace trainingmiddleware.Service
{
    public class UserService :IUser
    {
        private readonly DOTNETCOREContext _context;
        private readonly IConfiguration _configuration;
        public UserService(DOTNETCOREContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }
        public string Login(string email, string password)
        {
            var user = _context.User.SingleOrDefault(a => a.EmailId == email && a.Password == password);
            if (user == null)
            {
                return null;
            }
            // token needs to be generated 
            var secretkey = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWT"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,
                Audience = null,
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(5),
                Subject = new ClaimsIdentity(new List<Claim>
                { new Claim("UserId",user.UserId.ToString()),
                //new Claim("creationTime", user.)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretkey), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(jwtToken);
            return token;
        }
        public User Registration(string email, string password)
        {
            User userobj = new User();
            userobj.EmailId = email;
            userobj.Password = password;
            _context.User.Add(userobj);
            _context.SaveChanges();
            //if (user == null)
            //{
            //    return null;
            //}
            return userobj;
        }

    }
}
