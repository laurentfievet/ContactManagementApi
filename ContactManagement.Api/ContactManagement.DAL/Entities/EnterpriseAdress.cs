using System;
using System.ComponentModel.DataAnnotations;

namespace ContactManagement.DAL.Entities
{
    public class EnterpriseAdress
    {
        public long AdressId { get; set; }
        public long EnterpriseId { get; set; }
        [Required]
        public bool HeadOffice { get; set; }
        public virtual Adress Adress { get; set; }
        public virtual Enterprise Enterprise { get; set; }
    }
}
