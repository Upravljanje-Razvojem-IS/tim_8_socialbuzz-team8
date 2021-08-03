using Messaging.Entity.MockDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messaging.Mocks
{
    public class UserMock : IUserMock
    {
        public static List<UserDto> Users { get; set; } = new List<UserDto>();

        public UserMock()
        {
            FillData();
        }

        private void FillData()
        {
            Users.AddRange(new List<UserDto>
            {
                new UserDto
                {
                    UserId = 11,
                    Name = "Name 1",
                    LastName = "Last Name 1",
                    Address = "Address 1",
                    Email = "Email 1",
                    Telephone = "97310",
                    DateOfBirth = new DateTime(1/4/1993),
                    Description = "Description 1"
                },
                 new UserDto
                {
                    UserId = 13,
                    Name = "Name 2",
                    LastName = "Last Name 2",
                    Address = "Address 2",
                    Email = "Email 2",
                    Telephone = "917610",
                    DateOfBirth = new DateTime(8/4/1993),
                    Description = "Description 2"
                },
                new UserDto
                {
                    UserId = 14,
                    Name = "Name 3",
                    LastName = "Last Name 3",
                    Address = "Address 3",
                    Email = "Email 3",
                    Telephone = "096834902",
                    DateOfBirth = new DateTime(1/10/1983),
                    Description = "Description 3"
                }
            });
        }
        public UserDto GetUserById(int Id)
        {
            return Users.FirstOrDefault(e => e.UserId == Id);
        }
    }
}
