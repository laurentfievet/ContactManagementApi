﻿using AutoMapper;
using ContactManagement.DAL;
using ContactManagement.DAL.Entities;
using ContactManagement.Repo.Models;
using ContactManagement.Repo.Repositories;
using ContactManagement.Repo.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement.Repo.Services.Implementations
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private ContactDBContext _dbContext;

        public ContactService(IContactRepository contactRepository, ContactDBContext dbContext)
        {

            _contactRepository = contactRepository;
        }

        public async Task<IEnumerable<Contact>> ListAsync()
        {
            return await _contactRepository.GetAllAsync();
        }

        public async Task<Contact> GetByIdAsync(long id)
        {
            var item = await _contactRepository.GetByIdAsync(id);
            return item;
        }


        public async Task<Contact> SaveAsync(ContactDTO contactDTO)
        {
            if (contactDTO != null)
            {
                Contact contact = await _contactRepository.GetByIdAsync(contactDTO.Id);
                if (contact == null)
                    contact = new Contact();

                Mapper.Map<ContactDTO, Contact>(contactDTO, contact);

                await _contactRepository.UpsertAsync(contact);

                return contact;
            }
            else
                throw new ArgumentNullException(nameof(contactDTO));
        }

       public async Task DeleteAsync(long id)
        {
            var item = await _contactRepository.GetByIdAsync(id);

            if (item == null) throw new NotFoundException(id);


            await _contactRepository.DeleteAsync(item);
        }
    }
}
