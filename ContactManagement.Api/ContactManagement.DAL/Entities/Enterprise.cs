using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContactManagement.DAL.Entities
{
    public class Enterprise
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(20)]
        public string VATNumber { get; set; }

        public ICollection<EnterpriseAddress> EnterpriseAddress { get; set; } = new List<EnterpriseAddress>();
        public ICollection<ContactEnterprise> ContactEnterprise { get; set; } = new List<ContactEnterprise>();
    }
}
