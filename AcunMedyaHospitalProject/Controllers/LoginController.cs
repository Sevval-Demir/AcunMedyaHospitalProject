using AcunMedyaHospitalProject.Context;
using AcunMedyaHospitalProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AcunMedyaHospitalProject.Controllers
{
    public class LoginController : Controller
    {
        
        AppDbContext db = new AppDbContext();

        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Admin admin)
        {
            var value=db.Admins.FirstOrDefault(x => x.UserName == admin.UserName && x.Password == admin.Password);
            if(value== null)
            {
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                return View();
            }
            FormsAuthentication.SetAuthCookie(value.UserName, true);
            return RedirectToAction("Index","Doctor");
        }
    }
}