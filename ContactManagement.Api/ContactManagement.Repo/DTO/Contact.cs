﻿using System;
using System.Collections.Generic;

namespace ContactManagement.Repo.Models
{
    public class ContactDTO
    {
       
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GSMNumber { get; set; }
        public bool IsFreelance { get; set; }
        public string TVANumber { get; set; }
        public AdressDTO Adress { get; set; }
        public List<EnterpriseDTOBase> Enterprises { get; set; }
    }

   
}
