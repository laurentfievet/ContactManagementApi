using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ContactManagement.DAL.Entities
{
    public class ContactEnterprise
    {
       
        public long ContactId { get; set; }
        public long EnterpriseId { get; set; }

        [JsonIgnore]
        public virtual Contact Contact { get; set; }
        [JsonIgnore]
        public virtual Enterprise Enterprise { get; set; }
    }
}
