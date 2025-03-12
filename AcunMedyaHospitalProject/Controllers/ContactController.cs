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
            var contacts = db.Contacts.ToList();
            ViewBag.Contacts = contacts;

            if (!contacts.Any())  // Eğer hiç kayıt yoksa
            {
                ViewBag.Contact = null;
            }
            else
            {
                ViewBag.Contact = contacts.FirstOrDefault();
            }

            return View();
        }


        [HttpPost]
        public ActionResult CreateContact(Contact contact)
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
            if (contact.Id == 0)
            {
                return RedirectToAction("Index"); // ID eksikse hata vermemesi için
            }

            if (ModelState.IsValid)
            {
                var existingContact = db.Contacts.Find(contact.Id);
                if (existingContact != null)
                {
                    existingContact.Address = contact.Address;
                    existingContact.Phone = contact.Phone;
                    existingContact.Email = contact.Email;
                    existingContact.Longitude = contact.Longitude;
                    existingContact.Latitute = contact.Latitute;

                    db.Entry(existingContact).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
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