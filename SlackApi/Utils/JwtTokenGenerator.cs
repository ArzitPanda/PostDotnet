﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SlackApi.Utils
{
    public class JwtTokenGenerator
    {
        private static string _secretKey= "A9bC4dE7fG2hI5jK8lM1nO3pQ6rS0tUA9bC4dE7fG2hI5jK8lM1nO3pQ6rS0tUA9bC4dE7fG2hI5jK8lM1nO3pQ6rS0tUA9bC4dE7fG2hI5jK8lM1nO3pQ6rS0tUA9bC4dE7fG2hI5jK8lM1nO3pQ6rS0tU";

        

        public static  string GenerateToken(string clientId, string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, clientId),
                    new Claim(ClaimTypes.Name, userName),
                  
                }),
                Issuer = "http://localhost:5002", // Add issuer claim
                Audience = "http://localhost:5283", // Add audience claim
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
