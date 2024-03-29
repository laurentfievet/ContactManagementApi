﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContactManagement.DAL.Entities
{
    public class Contact
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(20)]
        public string GSMNumber { get; set; }
        [Required]
        public bool IsFreelance { get; set; }

        [StringLength(20)]
        public string VATNumber { get; set; }

        [Required]
        public long AddressId { get; set; }

        public virtual Address Address { get; set; }

        [JsonIgnore]
        public ICollection<ContactEnterprise> ContactEnterprise { get; set; } = new List<ContactEnterprise>();

    }
}
