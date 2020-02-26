using Course_Work.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Course_Work.Models
{
    public class OfferViewModel
    {
        [Required]
        public int OfferId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string UserPhone { get; set; }
        [Required]
        
        [Column(TypeName = "decimal")]
        public decimal Price { get; set; }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string IMGUrl { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public List<SelectListItem> Categories { get; set; }
        [Required]
        public int categoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public HttpPostedFileBase SomeFile { get; set; }
        public string FilePath { get; set; }
    }
}