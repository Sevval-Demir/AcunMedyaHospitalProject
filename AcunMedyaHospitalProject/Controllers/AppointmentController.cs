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
        public ActionResult DeleteAppointment(int id)
        {
            var value = db.Appointments.Find(id);
            db.Appointments.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateAppointment(int id)
        {
            var value = db.Appointments.Include("Doctor").Include("Department").FirstOrDefault(x => x.Id == id);
            TempData["Departments"]=new SelectList(db.Departments, "Id", "Name",value.DepartmentId);
            TempData["Doctors"] = new SelectList(db.Doctors.Where(x => x.DepartmentId == value.DepartmentId), "Id", "Name", value.Id);
            return View("UpdateAppointment",value);
        }

        [HttpPost]
        public ActionResult UpdateAppointment(Entities.Appointment appointment)
        {
            var value = db.Appointments.Find(appointment.Id);
            if(value==null)
            {
                return HttpNotFound();
            }
            var doctor = db.Doctors.Find(appointment.DoctorId);
            if(doctor == null)
            {
                ModelState.AddModelError("DoctorId", "Doctor not found");
                TempData["Departments"] = new SelectList(db.Departments, "Id", "Name", appointment.DepartmentId);
                TempData["Doctors"] = new SelectList(db.Doctors.Where(x => x.DepartmentId == appointment.DepartmentId), "Id", "FullName", appointment.DoctorId);
                return View("UpdateAppointment", appointment);
            }
            value.PatientFirstName = appointment.PatientFirstName;
            value.PatientLastName = appointment.PatientLastName;
            value.PatientEmail = appointment.PatientEmail;
            value.PatientPhone = appointment.PatientPhone;
            value.Date = appointment.Date;
            value.Time = appointment.Time;
            value.DepartmentId = appointment.DepartmentId;
            value.DoctorId = appointment.DoctorId;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetDoctors(int departmentId)
        {
            var doctors = db.Doctors.Where(x => x.DepartmentId == departmentId).ToList()
                .Select(a => new { a.Id, FullName = a.FirstName + " " + a.LastName }).ToList();
            return Json(doctors, JsonRequestBehavior.AllowGet);
        }

    }
}