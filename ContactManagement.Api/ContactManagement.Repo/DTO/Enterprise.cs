using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.Repo.Models
{
    public class EnterpriseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string TVANumber { get; set; }
        public List<EnterpriseAdressDTO> Adresses { get; set; }
        public List<ContactDTO> Contacts { get; set; }
    }
}
