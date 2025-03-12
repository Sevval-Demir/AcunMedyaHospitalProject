using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AcunMedyaHospitalProject.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsRead { get; set; }
    }
}