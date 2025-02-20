using AcunMedyaHospitalProject.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcunMedyaHospitalProject.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
       private readonly AppDbContext db=new AppDbContext();
        public ActionResult Index()
        {
            var values = db.Appointments.ToList();
            return View(values);
        }

        public ActionResult AppointmentByDoctor(int id)
        {
            var appointments = db.Appointments.Where(x => x.DoctorId == id).ToList();
            return View("Index", appointments);
        }
        public ActionResult ApproveAppointment(int id)
        {
            var value = db.Appointments.Find(id);
            value.Status=Enums.AppointmentStatus.Approved;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CancelAppointment(int id)
        {
            var value=db.Appointments.Find(id);
            value.Status =Enums.AppointmentStatus.Cancelled;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}