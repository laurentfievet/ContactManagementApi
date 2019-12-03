using ContactManagement.Api.Controllers;
using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;
using ContactManagement.Repo.Repositories;
using ContactManagement.Repo.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.xUnitTest.ServiceTest
{
    public class ContactServiceFake : IContactService
    {
        public readonly List<ContactDTO> _contactDTOList;
        public readonly List<Contact> _contactList;


        public ContactServiceFake()
        {
            _contactDTOList = new List<ContactDTO> {
                new ContactDTO{ Id=1, FirstName="Laurent", LastName = "FIEVET", GSMNumber="111515151", IsFreelance = true, VATNumber = "", Address = new AddressDTO{ Name= "test", City="", Country="", PostalCode="", Street="", StreetNumber=""} },
                 new ContactDTO{ Id=3, FirstName="pers1", LastName = "pers1", GSMNumber="111515151", IsFreelance = true, VATNumber = "15646411644", Address = new AddressDTO{ Name= "test1", City="dvfsv", Country="dvfsv", PostalCode="dvfsv", Street="dvfsv", StreetNumber="dvfsv"} },
                 new ContactDTO{ Id=5, FirstName="pers2", LastName = "pers2", GSMNumber="111515151", IsFreelance = false, VATNumber = "", Address = new AddressDTO{ Name= "test2", City="klkjkj", Country="klkjkj", PostalCode="klkjkj", Street="klkjkj", StreetNumber="klkjkj"} }
            };

            _contactList = new List<Contact> {
                new Contact{ Id=1, FirstName="Laurent", LastName = "FIEVET", GSMNumber="111515151", IsFreelance = true, VATNumber = "", Address = new Address{ Name= "test", City="", Country="", PostalCode="", Street="", StreetNumber=""} },
                 new Contact{ Id=3, FirstName="pers1", LastName = "pers1", GSMNumber="111515151", IsFreelance = false, VATNumber = "", Address = new Address{ Name= "test1", City="dvfsv", Country="dvfsv", PostalCode="dvfsv", Street="dvfsv", StreetNumber="dvfsv"} },
                 new Contact{ Id=5, FirstName="pers2", LastName = "pers2", GSMNumber="111515151", IsFreelance = false, VATNumber = "", Address = new Address{ Name= "test2", City="klkjkj", Country="klkjkj", PostalCode="klkjkj", Street="klkjkj", StreetNumber="klkjkj"} }
            };

          
        }

        public async Task<IEnumerable<Contact>> ListAsync()
        {
            return _contactList;
        }

        public async Task<ContactDTO> GetByIdAsync(long id)
        {
            return _contactDTOList.Where(a => a.Id == id)
               .FirstOrDefault();
        }


        public async Task<Contact> SaveAsync(ContactDTO contactDTO)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(long id)
        {
            var existing = _contactList.First(a => a.Id == id);
            _contactList.Remove(existing);
        }


    }
}
