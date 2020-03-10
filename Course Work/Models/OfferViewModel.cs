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
        [Required(ErrorMessage = "Це поле обов`язково")]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Це поле обов`язково")]
        [DataType(DataType.MultilineText)]
        [UIHint("DisplayPostalAddr")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Це поле обов`язково")]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        public string UserPhone { get; set; }
        [Required]
        
        [Column(TypeName = "decimal")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Це поле обов`язково")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public string UserID { get; set; }
        public List<SelectListItem> Categories { get; set; }
        public int categoryId { get; set; }
        public string CategoryName { get; set; }
        public HttpPostedFileBase SomeFile { get; set; }
        public string FilePath { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public int cityId { get; set; }
        public string cityName { get; set; }
        public bool IsVerified { get; set; }
    }


}