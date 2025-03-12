using AcunMedyaHospitalProject.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcunMedyaHospitalProject.Controllers
{
    public class AdminLayoutController : Controller
    {
        // GET: AdminLayout
        private readonly AppDbContext db= new AppDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PartialHeader()
        {
            return PartialView();
        }
        public ActionResult PartialSidebar()
        {
            return PartialView();
        }
        public ActionResult PartialNavbar()
        {
            // **Okunmamış mesajları çek**
            var unreadMessages = db.Messages.Where(m => !m.IsRead).ToList();
            ViewBag.UnreadMessageCount = unreadMessages.Count;
            ViewBag.UnreadMessages = unreadMessages.OrderByDescending(m => m.CreatedDate).Take(3).ToList(); // Son 3 mesajı göster

            // **Bekleyen randevuları çek**
            var pendingAppointments = db.Appointments
                .Where(a => a.Status == (int)Enums.AppointmentStatus.Pending)
                .ToList();
            ViewBag.PendingAppointmentCount = pendingAppointments.Count;
            ViewBag.PendingAppointments = pendingAppointments.OrderByDescending(a => a.Date).Take(3).ToList(); // Son 3 bekleyen randevu
            return PartialView();
        }
        public ActionResult PartialFooter()
        {
            return PartialView();
        }
        public ActionResult PartialScripts()
        {
            return PartialView();
        }

        public ActionResult Dashboard()
        {
            ViewBag.TotalDoctors = db.Doctors.Count();
            ViewBag.TotalDepartments = db.Departments.Count();
            ViewBag.TotalAppointments = db.Appointments.Count();

            ViewBag.PendingAppointments = db.Appointments.Count(a => a.Status == Enums.AppointmentStatus.Pending);
            ViewBag.ApprovedAppointments = db.Appointments.Count(a => a.Status == Enums.AppointmentStatus.Approved);
            ViewBag.CancelledAppointments = db.Appointments.Count(a => a.Status == Enums.AppointmentStatus.Cancelled);

            return View();
        }

        public JsonResult GetAppointmentStats()
        {
            var last6Months = db.Appointments
                .GroupBy(a => new { a.Date.Year, a.Date.Month })
                .Select(g => new
                {
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    Count = g.Count()
                })
                .OrderBy(g => g.Year).ThenBy(g => g.Month)
                .Take(6)
                .ToList();

            return Json(last6Months, JsonRequestBehavior.AllowGet);
        }


    }
}