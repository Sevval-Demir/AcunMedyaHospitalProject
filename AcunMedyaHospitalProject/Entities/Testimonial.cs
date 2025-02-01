using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcunMedyaHospitalProject.Entities
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string ImageUrl { get; set; }
    }
}