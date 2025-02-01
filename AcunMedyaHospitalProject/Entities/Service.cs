using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcunMedyaHospitalProject.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconName { get; set; }
        public string ButtonName { get; set; }
        public string ButtonLink { get; set; }
    }
}