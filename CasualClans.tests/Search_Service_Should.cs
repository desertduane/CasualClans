using CasualClans.Data;
using CasualClans.Data.Models;
using CasualClans.Service;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace CasualClans.tests
{
    [TestFixture]
    public class Post_Service_Should
    {
        [TestCase("games", 3)]
        [TestCase("yaY", 1)]
        [TestCase("water", 0)]
        public void Return_Filtered_Results_Corresponding_To_Query(string query, int expected)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using (var ctx = new ApplicationDbContext(options))
            {
                ctx.Forums.Add(new Forum
                {
                    Id = 10
                });

                ctx.Posts.Add(new Post
                {
                    Forum = ctx.Forums.Find(10),
                    Id = 23,
                    Title = "First Post",
                    Content = "Games"
                });

                ctx.Posts.Add(new Post
                {
                    Forum = ctx.Forums.Find(10),
                    Id = 100,
                    Title = "Second Post games",
                    Content = "yay"
                });

                ctx.Posts.Add(new Post
                {
                    Forum = ctx.Forums.Find(10),
                    Id = 231,
                    Title = "Last Post",
                    Content = "Games"
                });
                ctx.SaveChanges();
            }
            using(var ctx = new ApplicationDbContext(options))
            {
                var postService = new PostService(ctx);
                var result = postService.GetFilteredPosts(query);
                var postCount = result.Count();

                Assert.AreEqual(expected, postCount);
            }
        }
    }
}
