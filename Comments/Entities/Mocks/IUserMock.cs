using Comments.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comments.Entities.Mocks
{
   public interface IUserMock
    {
        UserDto GetUserById(int Id);
    }
}
