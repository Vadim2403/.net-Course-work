using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Course_Work.Entity
{
    [Table("tblResumes")]
    public class ResumeModel
    {
        [Key]
        public int Resume_ID { get; set; }
        [Required]
        public string User_Name { get; set; }
        [Required]
        public string User_Surname { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string User_Email { get; set; }
        public string Additionals { get; set; }
        [Required]
        public int Offer_Id { get; set; }
        [Required]
        public string User_Id { get; set; }
    }
}