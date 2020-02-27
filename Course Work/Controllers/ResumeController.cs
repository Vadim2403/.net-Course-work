using Course_Work.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Course_Work.Controllers
{
    public class ResumeController : Controller
    {
        // GET: Resume
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create(int id)
        {
            ResumeViewModel resume = new ResumeViewModel()
            {
               User_Id = User.Identity.GetUserId(),
               Offer_Id = id,
            };
            return View(resume);
        }
        [HttpPost]
        public ActionResult Create(ResumeViewModel model)
        {
            ApplicationDbContext _context = new ApplicationDbContext();
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
    }
}