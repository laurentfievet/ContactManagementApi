using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagement.Repo.Models
{
    public class AdressDTO
    {
       
        public long Id { get; set; }
        public string Name { get; set; }
        public string StreetNumber { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class EnterpriseAdressDTO : AdressDTO
    {
        public bool HeadOffice { get; set; }
    }

    public class EnterpriseAdresListDTO
    {
        public List<EnterpriseAdressDTO> enterpriseAdresses { get; set; }
    }
}
