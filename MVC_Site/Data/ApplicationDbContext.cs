using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Site.Models;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;

namespace MVC_Site.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<UserPost> userPosts;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserPost>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<UserPost>()
                .HasOne(x => x.User)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.Posts)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);
        }


        public IEnumerable<UserPost>? GetPosts()
        {
            /* return this.Set<UserPost>()
                 .Select(x => new UserPost
                 {
                     Id = x.Id,
                     User = this.Set<ApplicationUser>().Where(q => q.Id == x.UserId).First(),
                     UserId = x.UserId,
                     Body = x.Body,
                     Title = x.Title,
                     CreatedDate = x.CreatedDate,
                     UpdatedDate = x.UpdatedDate,

                 }); ;*/
            return this.Set<UserPost>().Include(x => x.User);
        }
    }
}
