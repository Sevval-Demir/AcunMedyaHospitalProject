using AcunMedyaHospitalProject.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcunMedyaHospitalProject.Controllers
{
    public class DefaultController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PartialSlider()
        {
            var slider = db.Sliders.ToList();
            return PartialView(slider);
        }
        public ActionResult PartialService()
        {
            var service=db.Services.ToList();
            return PartialView(service);
        }
        public ActionResult PartialDepartment()
        {
            var department = db.Departments.ToList();
            return PartialView(department);
        }
        public ActionResult PartialPatientComment()
        {
            var patientComments = db.PatientComments.ToList();
            return PartialView(patientComments);
        }
        public ActionResult PartialDoctor()
        {
            var doctor = db.Doctors.ToList();
            return PartialView(doctor);
        }
        public ActionResult PartialEmergency()
        {
            var contact = db.Contacts.FirstOrDefault();
            if(contact == null)
            {
                contact = new Entities.Contact();
            }
            return PartialView(contact);
        }

    }
}