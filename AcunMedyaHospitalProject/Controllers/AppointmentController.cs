using AcunMedyaHospitalProject.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace AcunMedyaHospitalProject.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();
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
            value.Status = Enums.AppointmentStatus.Approved;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CancelAppointment(int id)
        {
            var value = db.Appointments.Find(id);
            value.Status = Enums.AppointmentStatus.Cancelled;
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
            var appointment = db.Appointments.Include("Doctor").Include("Department").FirstOrDefault(a => a.Id == id);

            if (appointment == null)
            {
                return HttpNotFound("Randevu bulunamadı.");
            }

            ViewBag.Departments = new SelectList(db.Departments, "Id", "Name", appointment.DepartmentId);
            ViewBag.Doctors = new SelectList(db.Doctors.Where(d => d.DepartmentId == appointment.DepartmentId), "Id", "FullName", appointment.DoctorId);

            return View("UpdateAppointment", appointment);
        }

        [HttpPost]
        public ActionResult UpdateAppointment(Entities.Appointment appointment)
        {
            var value = db.Appointments.Find(appointment.Id);
            if (value == null)
            {
                // Randevu bulunamadı, uygun bir hata mesajı veya yönlendirme ekleyin
                return HttpNotFound("Randevu bulunamadı.");
            }

            // Geçerli bir DoctorId olup olmadığını kontrol edin
            var doctor = db.Doctors.Find(appointment.DoctorId);
            if (doctor == null)
            {
                // Geçersiz DoctorId, uygun bir hata mesajı veya yönlendirme ekleyin
                ModelState.AddModelError("DoctorId", "Geçersiz doktor seçimi.");
                TempData["Departments"] = new SelectList(db.Departments, "Id", "Name", appointment.DepartmentId);
                TempData["Doctors"] = new SelectList(db.Doctors.Where(d => d.DepartmentId == appointment.DepartmentId), "Id", "FullName", appointment.DoctorId);
                return View("UpdateAppointment", appointment);
            }

            value.PatientFirstName = appointment.PatientFirstName;
            value.PatientLastName = appointment.PatientLastName;
            value.PatientPhone = appointment.PatientPhone;
            value.PatientEmail = appointment.PatientEmail;
            value.Date = appointment.Date;
            value.Time = appointment.Time;
            value.DepartmentId = appointment.DepartmentId;
            value.DoctorId = appointment.DoctorId;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult CreateAppointment()
        {
            ViewBag.Departments = new SelectList(db.Departments, "Id", "Name");
            ViewBag.Doctors = new SelectList(Enumerable.Empty<SelectListItem>()); // Başlangıçta boş doktor listesi

            return View();
        }

        [HttpPost]
        public ActionResult CreateAppointment(Entities.Appointment appointment)
        {
            Console.WriteLine("Gelen Date: " + appointment.Date);
            Console.WriteLine("Gelen Time: " + appointment.Time);

            if (ModelState.IsValid)
            {
                // Eğer tarih veya saat boşsa, hata mesajı ekleyelim
                if (appointment.Date == DateTime.MinValue)
                {
                    ModelState.AddModelError("Date", "Geçerli bir tarih seçmelisiniz.");
                    return View(appointment);
                }

                if (appointment.Time == TimeSpan.Zero)
                {
                    ModelState.AddModelError("Time", "Geçerli bir saat seçmelisiniz.");
                    return View(appointment);
                }

                db.Appointments.Add(appointment);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(appointment);
        }


        [HttpGet]
        public JsonResult GetDoctorsByDepartment(int departmentId)
        {
            var doctors = db.Doctors.Where(d => d.DepartmentId == departmentId)
                                    .Select(d => new { d.Id, FullName = d.FirstName + " " + d.LastName })
                                    .ToList();
            return Json(doctors, JsonRequestBehavior.AllowGet);
        }

    }
}