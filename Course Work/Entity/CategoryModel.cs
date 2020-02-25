using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Course_Work.Entity
{
    [Table("tblCategory")]
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Category_name { get; set; }
    }
}