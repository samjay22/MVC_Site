using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC_Site.Models;

namespace MVC_Site
{
    public class PostDTO
    {
        public string Username { get; set; }
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public interface IPostService
    {
        IEnumerable<PostDTO> GetLatestPosts();
        public Task CreatePost(ApplicationUser user, NewPostModel post);
        public Task UpdatePost(int postId);
        public Task DeletePost(int postId);

    }
}
