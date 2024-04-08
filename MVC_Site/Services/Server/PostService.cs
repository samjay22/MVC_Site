using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_Site.Data;
using MVC_Site.Models;

namespace MVC_Site.Services.Server
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _applcationDbContext;
        public PostService(ApplicationDbContext dbContext) { 
            _applcationDbContext = dbContext;
        }
        public async Task CreatePost(ApplicationUser user, NewPostModel newPost)
        {
            var post = new UserPost();
            post.Title = newPost.Title;
            post.Body = newPost.Body;
            post.UserId = user.Id;
            await _applcationDbContext.AddAsync(post);
        }

        public Task DeletePost(int postId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostDTO> GetLatestPosts()
        {
            return _applcationDbContext.GetPosts()!.Select(x => new PostDTO
            {
                Title = x.Title,
                Body = x.Body,
                PostId = x.Id,
                CreatedDate = x.CreatedDate,
                Username = x.User.DisplayName,
            });
        }

        public Task UpdatePost(int postId)
        {
            throw new NotImplementedException();
        }
    }
}
