using AuthService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Repositories
{
    public interface IAuthInfoRepository
    {
        AuthInfo GetAuthInfoByUserId(Guid userId);
        AuthInfo CreateAuthInfo(AuthInfo authInfo);
        AuthInfo GetAuthInfoWithToken(string token);
        void UpdateAuthInfo(AuthInfo user);
        void DeleteAuthInfo(Guid userId);
        bool SaveChanges();
    }
}
