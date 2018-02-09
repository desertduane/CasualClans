﻿using CasualClans.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasualClans.Data
{
    public interface IForum
    {
        Forum GetById(int Id);
        IEnumerable<Forum> GetAll();
        IEnumerable<ApplicationUser> GetAllActiveUsers();

        Task Create(Forum forum);

    }
}