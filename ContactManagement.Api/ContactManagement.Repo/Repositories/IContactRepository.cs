﻿using ContactManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Repo.Repositories
{
    public interface IContactRepository : IRepository<Contact, long>
    {
        Task<List<Contact>> GetAllAsync();
    }
}