using Messaging.Entity.MockDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messaging.Mocks
{
    public interface IUserMock
    {
        UserDto GetUserById(int Id);
    }
}
