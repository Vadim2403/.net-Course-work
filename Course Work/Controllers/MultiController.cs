using Course_Work.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Course_Work.Controllers
{
    public class MultiController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Multi
        public ActionResult Index(string id)
        {


            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            List<ApplicationUser> UserCurrent = new List<ApplicationUser>();
            UserCurrent.Add(user);
            List<OfferViewModel> listOffers = new List<OfferViewModel>();
            foreach (var i in _context.offers.ToList())
            {
                if (i.UserID == user.Id)
                {
                    listOffers.Add(new OfferViewModel
                    {
                        Description = i.Description,
                        Email = i.Email,
                        IMGUrl = i.IMGUrl,
                        OfferId = i.Id,
                        Price = i.Price,
                        Title = i.Title,
                        UserID = i.UserID,
                        UserPhone = i.UserPhone,
                    });
                }
            }
            dynamic mymodel = new ExpandoObject();
            mymodel.User = UserCurrent;
            mymodel.Offers = listOffers;
            return View(mymodel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult MoreInfo(int Id)
        {
            OfferViewModel selectOffer = new OfferViewModel();
            var temp = _context.offers.FirstOrDefault(t => t.Id == Id);
            selectOffer.OfferId = temp.Id;
            selectOffer.Title = temp.Title;
            selectOffer.Price = temp.Price;
            selectOffer.IMGUrl = temp.IMGUrl;
            selectOffer.UserID = temp.UserID;
            selectOffer.Description = temp.Description;
            selectOffer.Email = temp.Email;
            selectOffer.UserPhone = temp.UserPhone;

            return View(selectOffer);
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(OfferViewModel model)
        {
            _context.offers.Add(new Entity.OfferModel
            {
                Description = model.Description,
                Email = model.Email,
                IMGUrl = model.IMGUrl,
                Price = model.Price,
                Title = model.Title,
                UserID = User.Identity.GetUserId(),
                UserPhone = model.UserPhone,
            });
            _context.SaveChanges();
            return RedirectToAction("Index", "Multi", new { id = User.Identity.GetUserId(), area = "" });
        }

        public ActionResult Delete(int id)
        {
            _context.offers.Remove(_context.offers.FirstOrDefault(x => x.Id == id));
            _context.SaveChanges();
            return RedirectToAction("Index", "Multi", new { id = User.Identity.GetUserId(), area = "" });
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var cOffer = _context.offers.FirstOrDefault(x => x.Id == id);
            OfferViewModel Offer = new OfferViewModel()
            {
                Description = cOffer.Description,
                Email = cOffer.Email,
                IMGUrl = cOffer.IMGUrl,
                OfferId = cOffer.Id,
                Price = cOffer.Price,
                Title = cOffer.Title,
                UserID = cOffer.UserID,
                UserPhone = cOffer.UserPhone,
            };
            return View(Offer);
        }
        [HttpPost]
        public ActionResult Edit(OfferViewModel model)
        {
            var offer = _context.offers.FirstOrDefault(x => x.Id == model.OfferId);
            offer.IMGUrl = model.IMGUrl;
            offer.Price = model.Price;
            offer.Title = model.Title;
            offer.UserID = model.UserID;
            offer.UserPhone = model.UserPhone;
            offer.Description = model.Description;
            offer.Email = model.Email;
            _context.SaveChanges();
            return RedirectToAction("Index", "Multi", new { id = User.Identity.GetUserId(), area = "" });
        }
    }
}