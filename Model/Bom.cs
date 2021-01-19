using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PriceQuationApi.Model
{
    public class Bom
    {
        [Key]
        [Display(Name = "總成件號")]
        public string AssemblyPartNumber { get; set; }

        [Required]
        [Display(Name = "總成件名(中文)")]
        public string AssemblyName { get; set; }

        [Display(Name = "總成件名(英文)")]
        public string AssemblyNameEng { get; set; }

        [Required]
        [Display(Name = "顧客")]
        public string Customer { get; set; }

        [Required]
        [Display(Name = "車型")]
        public string Model { get; set; }

        [Display(Name = "總成備註")]
        public string AssemblyRemark { get; set; }

        [Required]
        [Display(Name = "創立者")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "創立時間")]
        [Column(TypeName = "Date")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "修改時間")]
        [Column(TypeName = "Date")]
        public DateTime? ModifyDate { get; set; }

        [Required]
        [Display(Name = "報價完成時間(預估)")]
        [Column(TypeName = "Date")]
        public DateTime AllFinishTime { get; set; }

        [Required]
        [Display(Name = "狀態")]
        //1.Bom表匯入 , 2.量檢具需求填寫 3.夾治具需求填寫 , 
        //4.各單位報價  , 5.報價完成 , 6.作廢 , 7.獲得合約
        public int Status { get; set; }

        //[一對多]QuoteDetail
        public List<QuoteDetail> QuoteDetails { get; set; }

        //[一對多] BomItem
        public List<BomItem> BomItems { get; set; }

        //[一對多] BomItem
        public List<MeasuringItem> MeasuringItems { get; set; }
    }
}