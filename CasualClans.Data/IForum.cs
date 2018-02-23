using CasualClans.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasualClans.Data
{
    public interface IForum
    {
        Forum GetById(int Id);
        IEnumerable<Forum> GetAll();

        Task Create(Forum forum);
        Task Delete(int forumId);
        Task UpdateForumTitle(int forumId, string newTitle);
        Task UpdateForumDescription(int forumId, string newDescription);
        IEnumerable<ApplicationUser> GetAllActiveUsers(int id);
        bool HasRecentPost(int id);
    }
}
