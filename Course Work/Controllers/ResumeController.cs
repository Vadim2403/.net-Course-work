using Course_Work.Entity;
using Course_Work.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Course_Work.Controllers
{
    public class ResumeController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Resume
        public ActionResult Index(int id)
        {
            var list = _context.resumes.Where(x => x.Offer_Id == id);
            List<ResumeViewModel> listResumes = new List<ResumeViewModel>();
            foreach (var i in list)
            {
                listResumes.Add(new ResumeViewModel()
                {
                    User_Surname = i.User_Surname,
                    Additionals = i.Additionals,
                    Offer_Id = i.Offer_Id,
                    PhoneNumber = i.PhoneNumber,
                    User_Email = i.User_Email,
                    User_Id = i.User_Id,
                    User_Name = i.User_Name,
                });
            }
            return View(listResumes);
        }
        [HttpGet]
        public ActionResult Create(int id)
        {
            string uid = User.Identity.GetUserId();
            bool flag = false;
            List<ResumeModel> list = _context.resumes.Where(x => x.User_Id == uid)
                                       .Where(x => x.Offer_Id == id).ToList();
            if (list.Count > 0)
            {
                flag = true;
            }

            if (flag == false)
            {
                ResumeViewModel resume = new ResumeViewModel()
                {
                    User_Id = User.Identity.GetUserId(),
                    Offer_Id = id,
                };
                return View(resume);
            }
            else return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Create(ResumeViewModel model)
        {

            _context.resumes.Add(new Entity.ResumeModel
            {
                User_Surname = model.User_Surname,
                Additionals = model.Additionals,
                Offer_Id = model.Offer_Id,
                PhoneNumber = model.PhoneNumber,
                User_Email = model.User_Email,
                User_Id = model.User_Id,
                User_Name = model.User_Name,
            });
            _context.SaveChanges();
            return View();
        }
        public ActionResult Email(int id)
        {
            ResumeModel resume = _context.resumes.FirstOrDefault(x => x.Resume_ID == id);
            OfferModel currentOffer = _context.offers.FirstOrDefault(x => x.Id == resume.Offer_Id);
            ApplicationUser currentUser = _context.Users.FirstOrDefault(x => x.Id == currentOffer.UserID);
            //try
            //{
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("oasis.workua@gmail.com");
            message.To.Add(new MailAddress(resume.User_Email));
            message.Subject = "ANSWER FROM OASIS";
            var htmlString = $"<div style=\"background:greenyellow\"><h1 style=\"color:white\">Ваше резюме на роботу: \"{currentOffer.Title}\" було розглянуто, будь ласкавий звязатись із роботодавцем за контактами:</h1>    <h4 style=\"color:white\">Email: {currentUser.Email}</h4>    <h4 style=\"color:white\">Phonenumber: {currentUser.PhoneNumber}</h4></div>";
                message.IsBodyHtml = true;
            message.Body = htmlString;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("oasis.workua@gmail.com", "Qwerty-1");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);
            //}
            //catch (Exception) { return RedirectToAction("Index", "Offer"); }
            return RedirectToAction("Index", "Home");
        }
    }
}