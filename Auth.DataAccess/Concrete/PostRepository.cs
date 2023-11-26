using Auth.DataAccess.Abstract;
using Auth.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.DataAccess.Concrete
{
    public class PostRepository : IPostRepository
    {
      

        public  async Task<Post> CreatePost(Post post)
        {
            using(AuthDbContext db = new())
            {
                db.Posts.Add(post);

                await db.SaveChangesAsync();
            }
            return post;

        }

        public async Task<List<Post>> GetAllPost()
        {
            using(AuthDbContext db = new()) {
                return await db.Posts.ToListAsync();
            }
        }


        public async Task<Post> GetPostById(Guid postId)
        {
            using(AuthDbContext db = new())
            {
                var _post = await db.Posts.FirstOrDefaultAsync(p => p.PostId == postId);
                return _post;
            }
        }

        public async Task<List<Post>> GetUserPosts(Guid userId)
        {
            using(AuthDbContext db = new())
            {
                return await db.Posts.Where(p => p.userId == userId).ToListAsync();
            }
        }
    }
}
