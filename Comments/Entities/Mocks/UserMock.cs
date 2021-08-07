using Comments.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comments.Entities.Mocks
{
    public class UserMock : IUserMock
    {

        public static List<UserDto> Users { get; set; } = new List<UserDto>();

        public UserMock()
        {
            FillData();
        }

        private static void FillData()
        {
            Users.AddRange(new List<UserDto>
            {
                new UserDto
                {
                    UserId = 1,
                    Name = "Milana",
                    LastName = "Maksimovic",
                    Address = "Address 1",
                    Email = "Email 1",
                    Telephone = "4532529"
                },
                 new UserDto
                {
                    UserId = 2,
                    Name = "Milica",
                    LastName = "Antonic",
                    Address = "Address 2",
                    Email = "Email 2",
                    Telephone = "8578345"
                },
                new UserDto
                {
                    UserId = 3,
                    Name = "Ksenija",
                    LastName = "Maksimovic",
                    Address = "Address 3",
                    Email = "Email 3",
                    Telephone = "64753624"
                }
            });
        }

        public UserDto GetUserById(int Id)
        {
            return Users.FirstOrDefault(e => e.UserId == Id);

        }
    }
}
