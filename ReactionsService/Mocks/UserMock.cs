using ReactionsService.Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactionsService.Mocks
{
    public class UserMock : IUserMock
    {
        public static List<UserDTO> Users { get; set; } = new List<UserDTO>();

        public UserMock()
        {
            FillData();
        }

        private static void FillData()
        {
            Users.AddRange(new List<UserDTO>{
                new UserDTO
        {
          UserId = 1,
          Name = "NameOne",
          LastName = "LastnameOne",
          Address = "AddressOne",
          Email = "emailOne",
          Telephone = "123456"
        },
        new UserDTO
        {
          UserId = 2,
          Name = "NameTwo",
          LastName = "LastnameTwo",
          Address = "AddressTwo",
          Email = "emailTwo",
          Telephone = "123457"
        },
        new UserDTO
        {
          UserId = 3,
          Name = "NameThree",
          LastName = "LastnameThree",
          Address = "AddressThree",
          Email = "emailThree",
          Telephone = "123458"
        }
      });
        }

        public UserDTO GetUserById(int Id)
        {
            return Users.FirstOrDefault(e => e.UserId == Id);
        }
    }
}
