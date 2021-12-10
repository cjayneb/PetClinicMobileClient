using System;
using System.Collections.Generic;
using System.Text;

namespace XPetClinic_MobileFrontend.Models
{
    public class Client
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string telephone { get; set; }
        public List<Pet> pets { get; set; }
        public List<Visit> visits { get; set; }

    }
}
