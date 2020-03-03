using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Course_Work.Models
{
    public class ResumeViewModel
    {
        [Required]
        public int Resume_ID { get; set; }
        [Required]
        public string User_Name { get; set; }
        [Required]
        public string User_Surname { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string User_Email { get; set; }
        [DataType(DataType.MultilineText)]
        public string Additionals { get; set; }
        [Required]
        public int Offer_Id { get; set; }
        [Required]
        public string User_Id { get; set; }
        [Required]
        public bool IsEmailed { get; set; }
        [Required]
        public bool IsSelected { get; set; }
    }
}