using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContactManagement.DAL.Entities
{
    public class Adress
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string StreetNumber { get; set; }
        [Required]
        [StringLength(250)]
        public string Street { get; set; }
        [Required]
        [StringLength(10)]
        public string PostalCode { get; set; }
        [Required]
        [StringLength(150)]
        public string City { get; set; }
        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        public ICollection<EnterpriseAdress> EnterpriseAdress { get; set; } = new List<EnterpriseAdress>();

    }
}
