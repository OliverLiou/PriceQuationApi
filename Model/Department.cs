using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PriceQuationApi.Model
{
    public class Department
    {
        [Key]
        [Display(Name="部門名稱")]
        public string DepartmentId {get; set;}

        [Required]
        [Display(Name = "部門代號")]
        public string Code {get; set;}

        //[一對多]User 
        public List<AdminUser> Users {get; set;}

        //[一對多]QuoteItem
        public List<QuoteItem> QuoteItems {get; set;}
    }
}