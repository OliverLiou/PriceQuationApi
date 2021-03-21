using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PriceQuationApi.ViewModels
{
    public class VDepartment
    {
        [Key]
        [Display(Name="部門編號")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentId { get; set; }

        [Required]
        [Display(Name = "部門代號")]
        public string Code {get; set;}

        [Required]
        [Display(Name = "部門名稱")]
        public string Name {get; set;}

        //[一對多]User 
        public List<VUser> VUsers {get; set;}

        //[一對多]QuoteItem
        public List<VQuoteItem> VQuoteItems {get; set;}

    }
}