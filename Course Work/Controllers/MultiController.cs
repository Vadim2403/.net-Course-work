﻿using Course_Work.Entity;
using Course_Work.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
        public string Find_category(int id)
        {
            string category_name = _context.categories.First(x => x.Id == id).Category_name;
            return category_name;
        }
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
                        categoryId = i.categoryId,
                        CategoryName = Find_category(i.categoryId),
                        FilePath = Url.Content(Constants.OfferImagePath) + i.ImageName,
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
            selectOffer.categoryId = temp.categoryId;
            selectOffer.CategoryName = Find_category(temp.categoryId);

            List<OfferViewModel> OfferCurrent = new List<OfferViewModel>();
            OfferCurrent.Add(selectOffer);

            List<ResumeViewModel> listResumes = new List<ResumeViewModel>();
            foreach (var i in _context.resumes.ToList())
            {
                if (selectOffer.OfferId == i.Offer_Id)
                {
                    listResumes.Add(new ResumeViewModel
                    {
                        User_Surname = i.User_Surname,
                        Additionals = i.Additionals,
                        Offer_Id = i.Offer_Id,
                        PhoneNumber = i.PhoneNumber,
                        Resume_ID = i.Resume_ID,
                        User_Email = i.User_Email,
                        User_Id = i.User_Id,
                        User_Name = i.User_Name,
                        IsEmailed = i.IsEmailed,
                        IsSelected = i.IsSelected,
                    });
                }
            }
            dynamic mymodel = new ExpandoObject();
            mymodel.Offer = OfferCurrent;
            mymodel.Resumes = listResumes;
            return View(mymodel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<CategoryModel> categories = _context.categories.ToList();
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach(CategoryModel i in categories)
            {
                listItems.Add(new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Category_name,
                });
            }
            OfferViewModel model = new OfferViewModel();
            model.Categories = listItems;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(OfferViewModel model)
        {
            string link = string.Empty;
            string filename = Guid.NewGuid().ToString() + ".jpg";
            string image = Server.MapPath(Constants.OfferImagePath) +
                filename;
            using (Bitmap bmp = new Bitmap(model.SomeFile.InputStream))
            {
                var saveImage = ImageWorker.CreateImage(bmp, 450, 450);
                if (saveImage != null)
                {
                    saveImage.Save(image, ImageFormat.Jpeg);
                    link = Url.Content(Constants.OfferImagePath) +
                        filename;
                    
                }
            }
                    _context.offers.Add(new Entity.OfferModel
            {
                Description = model.Description,
                Email = model.Email,
                IMGUrl = model.IMGUrl,
                Price = model.Price,
                Title = model.Title,
                UserID = User.Identity.GetUserId(),
                UserPhone = model.UserPhone,
                categoryId = model.categoryId,
                CategoryName = Find_category(model.categoryId),
                ImageName = filename,
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
                categoryId = cOffer.categoryId,
                CategoryName = Find_category(cOffer.categoryId),
                FilePath = Url.Content(Constants.OfferImagePath) + cOffer.ImageName,
            };
            List<CategoryModel> categories = _context.categories.ToList();
            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (CategoryModel i in categories)
            {
                listItems.Add(new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Category_name,
                });
            }
            Offer.Categories = listItems;
            return View(Offer);
        }

        [HttpPost]
        public ActionResult Edit(OfferViewModel model)
        {
            string filename = string.Empty;
            if (model.SomeFile != null)
            {
                string link = string.Empty;
                filename = Guid.NewGuid().ToString() + ".jpg";
                string image = Server.MapPath(Constants.OfferImagePath) +
                    filename;
                using (Bitmap bmp = new Bitmap(model.SomeFile.InputStream))
                {
                    var saveImage = ImageWorker.CreateImage(bmp, 450, 450);
                    if (saveImage != null)
                    {
                        saveImage.Save(image, ImageFormat.Jpeg);
                        link = Url.Content(Constants.OfferImagePath) +
                            filename;

                    }
                }
            }
            var offer = _context.offers.FirstOrDefault(x => x.Id == model.OfferId);
            offer.IMGUrl = model.IMGUrl;
            offer.Price = model.Price;
            offer.Title = model.Title;
            offer.UserID = model.UserID;
            offer.UserPhone = model.UserPhone;
            offer.Description = model.Description;
            offer.Email = model.Email;
            offer.categoryId = model.categoryId;
            offer.CategoryName = Find_category(offer.categoryId);
            if (model.SomeFile != null)
            {
                offer.ImageName = filename;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Multi", new { id = User.Identity.GetUserId(), area = "" });
        }
    }
}