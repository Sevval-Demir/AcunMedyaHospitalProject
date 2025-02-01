using AcunMedyaHospitalProject.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcunMedyaHospitalProject.Controllers
{
    private readonly AppDbContext db = new AppDbContext();
    public class DepartmentController : Controller
    {
        // GET: Department
        public ActionResult Index()
        {
            var departments = db.Departments.ToList();
            return View(departments);
        }
        [HttpGet]
        public ActionResult CreateDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDepartment(Department department)
        {
            if(ModelState.IsValid)
            {
               db.Departments.Add(department);
                db.SaveChanges();
                return RedirectToAction("Index","Department");
            }
            return View(department);
        }
}