using ReactionsService.Entity.DTO;
using System.Collections.Generic;
using System.Linq;


namespace ReactionsService.Mocks
{
    public class PostMock : IPostMock
    {
        public static List<PostDTO> Posts { get; set; } = new List<PostDTO>();

        public PostMock()
        {
            FillData();
        }

        private static void FillData()
        {
            Posts.AddRange(new List<PostDTO>
            {
                new PostDTO
                {
                    PostId = 1,
                    Title = "Title 1"
                },
                new PostDTO
                {
                    PostId = 2,
                    Title = "Title 2"
                },
                new PostDTO
                {
                    PostId = 3,
                    Title = "Title 3"
                }
            });
        }

        public PostDTO GetPostById(int Id)
        {
            return Posts.FirstOrDefault(e => e.PostId == Id);
        }
    }
}
