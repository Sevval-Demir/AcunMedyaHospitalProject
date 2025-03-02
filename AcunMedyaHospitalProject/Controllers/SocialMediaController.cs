using System.Linq;
using System.Web.Mvc;
using AcunMedyaHospitalProject.Context;
using AcunMedyaHospitalProject.Entities;

namespace AcunMedyaHospitalProject.Controllers
{
    public class SocialMediaController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {
            var socialMedias = db.SocialMedias.ToList();
            return View(socialMedias);
        }

        public ActionResult GetSocialMediaLinks()
        {
            var socialMedias = db.SocialMedias.ToList();
            return View(socialMedias);
        }


    }
}