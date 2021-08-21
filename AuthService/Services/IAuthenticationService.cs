using AuthService.Entities;
using AuthService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Login(Principal principal);
        //AuthenticationResponse GetAccessToken(Guid token);
        void Logout(Guid userId);
        AuthInfo GetAuthInfoByUserId(Guid id);
        AuthInfo GetAuthInfoByToken(string token);
    }
}
