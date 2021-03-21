using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PriceQuationApi.ViewModels
{
    public class VBom
    {
        [Key]
        [Display(Name = "BomId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BomId { get; set; }

        [Required]
        [Display(Name = "Oppo編號")]
        public string OppoId { get; set; }

        [Key]
        [Display(Name = "總成件號")]
        public string AssemblyPartNumber { get; set; }

        [Display(Name = "總成件名(中文)")]
        public string AssemblyName { get; set; }

        [Display(Name = "總成件名(英文)")]
        public string AssemblyNameEng { get; set; }

        [Display(Name = "顧客")]
        public string Customer { get; set; }

        [Display(Name = "車型")]
        public string Model { get; set; }

        [Display(Name = "總成備註")]
        public string AssemblyRemark { get; set; }

        [Display(Name = "創立者")]
        public int UserId { get; set; }

        [Display(Name = "創立時間")]
        [Column(TypeName = "Date")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "修改時間")]
        [Column(TypeName = "Date")]
        public DateTime? ModifyDate { get; set; }

        [Display(Name = "報價完成時間(預估)")]
        [Column(TypeName = "Date")]
        public DateTime? AllFinishTime { get; set; }
        
        //[一對多]OPPO
        public VOppo VOppo { get; set; }

        //[一對多]QuoteDetail
        public List<VQuoteDetail> VQuoteDetails { get; set; }

        //[一對多] BomItem
        public List<VBomItem> VBomItems { get; set; }

        //[一對多] MeasuringItem
        public List<VMeasuringItem> VMeasuringItems { get; set; }

        //[一對多] FixtureItem
        public List<VFixtureItem> VFixtureItems { get; set; }
    }
}