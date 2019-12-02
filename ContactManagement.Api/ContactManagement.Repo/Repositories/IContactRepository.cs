using ContactManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.Repo.Repositories
{
    public interface IContactRepository : IRepository<Contact, long>
    {

    }
}
