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
        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CreateDoctor()
        {
            TempData["Departments"] = DepartmentHelper.GetDepartments();
            return View();
        }
    }
}