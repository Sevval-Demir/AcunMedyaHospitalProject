using AcunMedyaHospitalProject.Context;
using AcunMedyaHospitalProject.Entities;
using System.Linq;
using System.Web.Mvc;

namespace AcunMedyaHospitalProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

        // GET: Contact
        public ActionResult Index()
        {
            ViewBag.Contacts = db.Contacts.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult UpdateContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        [HttpPost]
        public ActionResult DeleteContact(int id)
        {
            var contact = db.Contacts.Find(id);
            if (contact != null)
            {
                db.Contacts.Remove(contact);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}