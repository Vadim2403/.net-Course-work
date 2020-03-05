using Course_Work.Entity;
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
        public string Find_city(int id)
        {
            string city_name = _context.cities.First(x => x.Id == id).City_name;
            return city_name;
        }
        public ActionResult Index()
        {
            List<CategoryModel> categories = _context.categories.ToList();
            List<CityModel> cities = _context.cities.ToList();
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<SelectListItem> listItems2 = new List<SelectListItem>();
            foreach (CategoryModel i in categories)
            {
                listItems.Add(new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Category_name,
                });
            }
            foreach (CityModel i in cities)
            {
                listItems2.Add(new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.City_name,
                });
            }
            List<OfferViewModel> list = new List<OfferViewModel>();
            foreach(var i in _context.offers.ToList())
            {
                list.Add(new OfferViewModel {
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
                    cityId = i.cityId,
                    cityName = Find_city(i.cityId),
                    Categories = listItems,
                    Cities = listItems2,
                });
            }
            return View(list);
        }
        public ActionResult IndexSearch(string SearchText, string CityId, string CategoryId)
        {
            List<CategoryModel> categories = _context.categories.ToList();
            List<CityModel> cities = _context.cities.ToList();
            List<SelectListItem> listItems = new List<SelectListItem>();
            List<SelectListItem> listItems2 = new List<SelectListItem>();

            var cityid = Convert.ToInt32(CityId);
            var categoryid = Convert.ToInt32(CategoryId);

            foreach (CategoryModel i in categories)
            {

                if (i.Id == categoryid)
                {
                    listItems.Add(new SelectListItem
                    {
                        Value = i.Id.ToString(),
                        Text = i.Category_name,
                        Selected = true
                    });
                }
                else
                {
                    listItems.Add(new SelectListItem
                    {
                        Value = i.Id.ToString(),
                        Text = i.Category_name,
                        Selected = false

                    });
                }

              
            }
            foreach (CityModel i in cities)
            {
                listItems2.Add(new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.City_name,
                });
            }
         
            var offers = _context.offers.ToList();
            string cityname = Find_city(cityid);
            string categoryname = Find_category(categoryid);
            List<OfferViewModel> list = new List<OfferViewModel>();
            foreach (var i in offers)
            {
                if (i.Title.Contains(SearchText) && i.cityName == cityname && i.CategoryName == categoryname)
                {
                    list.Add(new OfferViewModel
                    {
                        Description = i.Description,
                        Email = i.Email,
                        IMGUrl = i.IMGUrl,
                        Price = i.Price,
                        Title = i.Title,
                        UserPhone = i.UserPhone,
                        UserID = i.UserID,
                        OfferId = i.Id,
                        categoryId = categoryid,
                        CategoryName = Find_category(i.categoryId),
                        FilePath = Url.Content(Constants.OfferImagePath) + i.ImageName,
                        cityId = cityid,
                        cityName = Find_city(i.cityId),
                        Categories = listItems,
                        Cities = listItems2,
                    });
                }
            }
            if(list.Count <= 0)
            {
                list.Add(new OfferViewModel()
                {
                    Categories = listItems,
                    categoryId = categoryid,
                    Cities = listItems2,
                    cityId = cityid,
                });
            }
            return View(list);
        }


        public ActionResult Search(string searchText, string cityId, string categoryId)
        {
           
            return RedirectToAction("IndexSearch", "Offer",new { SearchText = searchText, CityId = cityId, CategoryId = categoryId, });

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