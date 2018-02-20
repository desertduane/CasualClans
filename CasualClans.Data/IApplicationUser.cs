using CasualClans.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CasualClans.Data
{
    public interface IApplicationUser
    {
        ApplicationUser GetById(string Id);
        IEnumerable<ApplicationUser> GetAll();

        Task SetProfileImage(string Id, Uri uri);
        Task UpdateUserRating(string Id, Type type);

    }
}
