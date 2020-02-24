using Course_Work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Course_Work.Controllers
{
    public class OfferController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Offer
        public ActionResult Index()
        {
            List<OfferViewModel> list = new List<OfferViewModel>();
            foreach(var i in _context.offers.ToList())
            {
                list.Add(new OfferViewModel{
                    Description = i.Description,
                    Email = i.Email,
                    IMGUrl = i.IMGUrl,
                    Price = i.Price,
                    Title = i.Title,
                    UserPhone = i.UserPhone,
                    UserID = i.UserID,
                    OfferId = i.Id
                });
            }
            return View(list);
        }

        [HttpGet]
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

    }
}