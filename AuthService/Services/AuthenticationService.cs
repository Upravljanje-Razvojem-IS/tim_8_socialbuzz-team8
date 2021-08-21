using AuthService.Entities;
using AuthService.Models;
using AuthService.Options;
using AuthService.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;
        private readonly IAuthInfoRepository _authRepository;
        private readonly string _userServiceUrl = "http://localhost:60001/api/Accounts";

        public AuthenticationService(IConfiguration configuration, JwtSettings jwtSettings, IAuthInfoRepository authRepository)
        {
            _configuration = configuration;
            _jwtSettings = jwtSettings;
            _authRepository = authRepository;
        }

        //public AuthenticationResponse GetAccessToken(Guid token)
        //{
        //    AuthInfo authInfo =_authRepository.GetAuthInfoWithToken(token);
        //    if(authInfo != null)
        //    {
        //        string token = IssueToken(authInfo.Id.ToString(), authInfo.Role);
        //        authInfo.PrivateToken = privateToken;
        //        _authRepository.SaveChanges();
        //        return new AuthenticationResponse
        //        {
        //            Token = privateToken,
        //            Succes = true
        //        };
        //    }
        //    return new AuthenticationResponse
        //    {
        //        Error = "Public token not found",
        //        Succes = false
        //    };
        //}

        public AuthInfo GetAuthInfoByToken(string token)
        {
            return _authRepository.GetAuthInfoWithToken(token);
        }

        public AuthInfo GetAuthInfoByUserId(Guid id)
        {
            return _authRepository.GetAuthInfoByUserId(id);
        }

        public async Task<AuthenticationResponse> Login(Principal principal)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsJsonAsync(_userServiceUrl, principal).Result;
                CheckPrincipalResponse res = await response.Content.ReadFromJsonAsync<CheckPrincipalResponse>();
                if (!response.IsSuccessStatusCode)
                {
                    return new AuthenticationResponse {
                        Success = false,
                        Error = res.Message.ToString() 
                    };
                }

                Guid id = res.AccountInfo.Id;
                string role = res.AccountInfo.Role;
                DateTime dateIssued = DateTime.UtcNow;
                AuthInfo user = _authRepository.GetAuthInfoByUserId(id);
                if(user != null)
                {
                    return new AuthenticationResponse
                    {
                        Token = user.Token,
                        Success = true
                    };
                }
                var token = IssueToken(id.ToString(), role);
                AuthInfo authInfo = new AuthInfo
                {
                    Id = id,
                    Role = role,
                    Token = token,
                    TimeOfIssuingToken = dateIssued
                };
                _authRepository.CreateAuthInfo(authInfo);
                return new AuthenticationResponse
                {
                    Token = token,
                    Success = true
                };               
            }

        }

        public void Logout(Guid userId)
        {
            _authRepository.DeleteAuthInfo(userId);
        }

        private string IssueToken(string userId, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                    {
                            new Claim("id", userId),
                            new Claim(ClaimTypes.Role, role)
                        }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
