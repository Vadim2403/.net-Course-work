using Course_Work.Areas.Admin.Models;
using Course_Work.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Course_Work.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            List<userModel> list = new List<userModel>();
            foreach(var i in _context.Users.ToList())
            {
                if (User.Identity.GetUserId() != i.Id)
                list.Add(new userModel
                {
                    Id = i.Id,
                    Name = i.UserName,
                    Email = i.Email,
                    Password = i.PasswordHash,
                    PhoneNumber = i.PhoneNumber,
                });
            }
            return View(list);
        }
        public ActionResult Delete(string id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if(user != null)
            {
              

                var useroffers = _context.offers.Where(t => t.UserID == id).ToList();


                foreach (var item in useroffers)
                {
                    var AllResumes = _context.resumes.Where(x => x.Offer_Id == item.Id);
                    foreach(var item2 in AllResumes)
                    {
                        _context.resumes.Remove(item2);
                    }
                    _context.offers.Remove(item);
                }
                var allUsersResumes = _context.resumes.Where(x => x.User_Id == id).ToList();


                foreach (var item in allUsersResumes)
                {
                    _context.resumes.Remove(item);
                }
                _context.Users.Remove(user);
                _context.SaveChanges();
   
            }
            return RedirectToAction("Index", "Admin", new { area = "Admin" });
        }
    }
}