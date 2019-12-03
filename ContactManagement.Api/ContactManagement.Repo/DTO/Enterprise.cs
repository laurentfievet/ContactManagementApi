using System.Collections.Generic;

namespace ContactManagement.Repo.Models
{
    public class EnterpriseDTOBase
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string TVANumber { get; set; }
    }

    public class EnterpriseDTOFull : EnterpriseDTOBase
    {
        public List<EnterpriseAdressDTO> Adresses { get; set; }
    }
}
