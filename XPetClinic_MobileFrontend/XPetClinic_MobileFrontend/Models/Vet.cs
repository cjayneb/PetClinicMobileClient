using System;
using System.Collections.Generic;
using System.Text;

namespace XPetClinic_MobileFrontend.Models
{
    public class Vet
    {
        public int vetId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string resume { get; set; }
        public string workday { get; set; }
        public int isActive { get; set; }
    }
}
