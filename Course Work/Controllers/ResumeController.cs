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
        ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Resume
        public ActionResult Index(int id)
        {
            var list = _context.resumes.Where(x => x.Offer_Id == id);
            List<ResumeViewModel> listResumes = new List<ResumeViewModel>();
            foreach(var i in list)
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