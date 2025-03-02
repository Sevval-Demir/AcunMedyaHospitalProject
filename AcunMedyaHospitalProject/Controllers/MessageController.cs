using AcunMedyaHospitalProject.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcunMedyaHospitalProject.Controllers
{
    public class MessageController : Controller
    {
        private readonly AppDbContext db=new AppDbContext();
        // GET: Message
        public ActionResult Index()
        {
            var pendingAppointments = db.Appointments.Where(x => x.Status == Enums.AppointmentStatus.Pending).ToList();
            return View(pendingAppointments);
        }
    }
}