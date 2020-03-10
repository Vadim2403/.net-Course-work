using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Course_Work.Entity
{
    [Table("tblOffers")]
    public class OfferModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string UserPhone { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public int categoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string ImageName { get; set; }
        [Required]
        public int cityId { get; set; }
        [Required]
        public string cityName { get; set; }

        public bool IsVerified { get; set; }
    }
}