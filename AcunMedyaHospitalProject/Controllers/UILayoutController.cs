using AcunMedyaHospitalProject.Context;
using AcunMedyaHospitalProject.Entities;
using AcunMedyaHospitalProject.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcunMedyaHospitalProject.Controllers
{
    public class UILayoutController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();
        // GET: UILayout
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PartialHeader()
        {
            return PartialView();
        }
        public ActionResult PartialSiteHeader()
        {
            return PartialView();
        }
        public ActionResult PartialFooter()
        {
            return PartialView();
        }
        public ActionResult PartialAppointmentForm()
        {
            TempData["Departments"] = DepartmentHelper.GetDepartments();
            return PartialView();
        }
        public ActionResult PartialScripts()
        {
            return PartialView();
        }
        [HttpGet]
        public JsonResult GetDoctorByDepartmentId(int departmentId)
        {
            var doctors = db.Doctors.Where(x => x.DepartmentId == departmentId)
                .Select(x => new { x.Id, x.FirstName, x.LastName })
                .ToList();
            return Json(doctors, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult MakeAppointment(Appointment appointment)
        {
            appointment.Status = Enums.AppointmentStatus.Pending;
            appointment.CreatedDate = DateTime.UtcNow;

            db.Appointments.Add(appointment);
            db.SaveChanges();
            return RedirectToAction("Index","Default");
        }

    }

}
