using ReactionsService.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Mocks
{
    public interface IUserMock
    {
        UserDTO GetUserById(int Id);
    }
}
