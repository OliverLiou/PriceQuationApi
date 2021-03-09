using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PriceQuationApi.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name="使用者編碼")]
        public int UserId { get; set; }

        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "帳號")]
        [StringLength(10 ,MinimumLength=4,ErrorMessage="帳號長度，最短必須為4但不可超過10")]
        public string Account {get; set;}

        [Required]
        [Display(Name = "密碼")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "密碼長度，最短必須為6但不可超過12")]
        [DataType(DataType.Password)]
        public string PassWord {get; set;}

        [Display(Name = "存活")]
        public bool Alive{get; set;}

        [Required]
        [Display(Name = "部門編號")]
        public int DepartmentId {get; set;}

        //[一對多] Department
        public Department Department {get; set;}

        //[一對多] QuoteDetail
        public List<QuoteDetail> QuoteDetails {get; set;}

    }
}