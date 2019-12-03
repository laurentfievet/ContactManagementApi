using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContactManagement.DAL.Entities
{
    public class EnterpriseAddress
    {
        public long AddressId { get; set; }
        public long EnterpriseId { get; set; }
        [Required]
        public bool HeadOffice { get; set; }

        public virtual Address Address { get; set; }
        [JsonIgnore]
        public virtual Enterprise Enterprise { get; set; }
    }
}
