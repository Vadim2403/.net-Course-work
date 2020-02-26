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
        public string Find_category(int id)
        {
            string category_name = _context.categories.First(x => x.Id == id).Category_name;
            return category_name;
        }
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
                    OfferId = i.Id,
                    categoryId = i.categoryId,
                    CategoryName = Find_category(i.categoryId),
                    FilePath = Url.Content(Constants.OfferImagePath) + i.ImageName,
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
            selectOffer.categoryId = temp.categoryId;
            selectOffer.CategoryName = Find_category(temp.categoryId);

            return View(selectOffer);
        }

    }
}