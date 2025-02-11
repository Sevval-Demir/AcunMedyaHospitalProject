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
    [Authorize]
    public class DoctorController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();
        // GET: Doctor
        public ActionResult Index()
        {
            var doctors = db.Doctors.ToList();
            return View(doctors);
        }

        public ActionResult DoctorByDepartment(int id)
        {
            var doctors = db.Doctors.Where(x => x.DepartmentId == id).ToList();
            return View("Index",doctors);
        }
       
        [HttpGet]
        public ActionResult CreateDoctor()
        {
            TempData["Departments"] = DepartmentHelper.GetDepartments();
            return View();
        }
        [HttpPost]
        public ActionResult CreateDoctor(Doctor doctor)
        {
            if(ModelState.IsValid)
            {
                db.Doctors.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("Index", "Doctor");
            }
            TempData["Departments"] = DepartmentHelper.GetDepartments();
            return View(doctor);
        }
    }
}