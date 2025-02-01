using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace AcunMedyaHospitalProject.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonIgnore]
        public string FullName { get { return FirstName + " " + LastName; } }
        public string ImageUrl { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

    }
}