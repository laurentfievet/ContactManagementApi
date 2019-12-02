using System;
using System.ComponentModel.DataAnnotations;

namespace ContactManagement.DAL.Entities
{
    public class ContactEnterprise
    {
       
        public long ContactId { get; set; }
        public long EnterpriseId { get; set; }

        public virtual Contact Contact { get; set; }
        public virtual Enterprise Enterprise { get; set; }
    }
}
