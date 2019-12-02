using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public string TVANumber { get; set; }

        public ICollection<EnterpriseAdress> EnterpriseAdress { get; set; } = new List<EnterpriseAdress>();
        public ICollection<ContactEnterprise> ContactEnterprise { get; set; } = new List<ContactEnterprise>();
    }
}
