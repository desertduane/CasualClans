using CasualClans.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasualClans.Data
{
    public interface IPost
    {
        Post GetById(int Id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetFilteredPosts(string searchQuery);

        Task Add(Post post);
        Task Delete(int Id);
        Task EditPostContent(int Id, string newContent);

        Task AddReply(PostReply reply);
    }
}
