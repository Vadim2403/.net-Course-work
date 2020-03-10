using Course_Work.Areas.Admin.Models;
using Course_Work.Entity;
using Course_Work.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Course_Work.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public string Find_category(int id)
        {
            string category_name = _context.categories.First(x => x.Id == id).Category_name;
            return category_name;
        }
        public string Find_city(int id)
        {
            string city_name = _context.cities.First(x => x.Id == id).City_name;
            return city_name;
        }
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
        public ActionResult Email(string email)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("oasis.workua@gmail.com");
                message.To.Add(new MailAddress(email));
                message.Subject = "Інструкція";
                var htmlString = $"''Інформація''";
                message.IsBodyHtml = true;
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("oasis.workua@gmail.com", "Qwerty-1");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) { return RedirectToAction("Index","Admin"); }
            return RedirectToAction("Index", "Admin");
        }
     [HttpGet]
        public ActionResult ShowOffers(string id)
        {
            List<OfferViewModel> list = new List<OfferViewModel>();
            foreach (var i in _context.offers.ToList())
            {
                if (id == i.UserID)
                {
                    list.Add(new OfferViewModel
                    {
                        categoryId = i.categoryId,
                        CategoryName = Find_category(i.categoryId),
                        cityId = i.cityId,
                        cityName = Find_city(i.cityId),
                        Description = i.Description,
                        Email = i.Email,
                        FilePath = Url.Content(Constants.OfferImagePath) + i.ImageName,
                        OfferId = i.Id,
                        Price = i.Price,
                        Title = i.Title,
                        UserID = i.UserID,
                        UserPhone = i.UserPhone,
                        IsVerified = i.IsVerified,
                    });
                }
            }
            return View(list);
        }
        public ActionResult Verify(int id)
        {
            var of = _context.offers.FirstOrDefault(x => x.Id == id);
            of.IsVerified = true;
            _context.SaveChanges();
            return RedirectToAction("ShowOffers","Admin",new { @id = of.UserID});
        }
        public ActionResult DeVerify(int id)
        {
            var of = _context.offers.FirstOrDefault(x => x.Id == id);
            of.IsVerified = false;
            _context.SaveChanges();
            return RedirectToAction("ShowOffers", "Admin", new { @id = of.UserID });
        }
    }
}