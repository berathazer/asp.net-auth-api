using Auth.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.DataAccess.Abstract
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllPost();

        Task <Post> GetPostById(Guid postId);

        Task <List<Post>> GetUserPosts(Guid userId);

        Task <Post> CreatePost(Post post);


    }
}
