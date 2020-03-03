using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Course_Work.Entity
{
    [Table("tblCities")]
    public class CityModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string City_name { get; set; }
    }
}