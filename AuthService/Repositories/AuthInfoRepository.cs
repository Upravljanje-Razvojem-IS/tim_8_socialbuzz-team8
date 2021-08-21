using AuthService.Entites;
using AuthService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Repositories
{
    public class AuthInfoRepository : IAuthInfoRepository
    {
        private readonly AuthDbContext _authDbContext;
        public AuthInfoRepository(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        public AuthInfo CreateAuthInfo(AuthInfo authInfo)
        {
            var createdAuthInfo = _authDbContext.AuthInfo.Add(authInfo);
            _authDbContext.SaveChanges();
            return createdAuthInfo.Entity;
        }

        public void DeleteAuthInfo(Guid userId)
        {
            var authInfo = _authDbContext.AuthInfo.FirstOrDefault(i => i.Id == userId);
            _authDbContext.Remove(authInfo);
            _authDbContext.SaveChanges();
        }

        public AuthInfo GetAuthInfoWithToken(string token)
        {
            return _authDbContext.AuthInfo.FirstOrDefault(i => i.Token.Equals(token.ToString()));
        }

        public AuthInfo GetAuthInfoByUserId(Guid userId)
        {
           return _authDbContext.AuthInfo.FirstOrDefault(i => i.Id == userId);
        }

        public bool SaveChanges()
        {
            return _authDbContext.SaveChanges() > 0;
        }

        public void UpdateAuthInfo(AuthInfo user)
        {
            throw new NotImplementedException();
        }
    }
}
