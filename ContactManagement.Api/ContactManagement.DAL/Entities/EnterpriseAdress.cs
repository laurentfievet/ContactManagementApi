using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContactManagement.DAL.Entities
{
    public class EnterpriseAdress
    {
        public long AdressId { get; set; }
        public long EnterpriseId { get; set; }
        [Required]
        public bool HeadOffice { get; set; }

        public virtual Adress Adress { get; set; }
        [JsonIgnore]
        public virtual Enterprise Enterprise { get; set; }
    }
}
