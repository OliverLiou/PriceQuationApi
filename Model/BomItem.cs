using System.ComponentModel.DataAnnotations;

namespace PriceQuationApi.Model
{
    public class BomItem
    {
        [Key]
        [Display(Name = "編號")]
        public string No { get; set; }

        [Required]
        [Display(Name = "總成編號")]
        public string AssemblyPartNumber { get; set; }

        [Required]
        [Display(Name = "構成關係")]
        public int PartLevel { get; set; }

        [Required]
        [Display(Name = "件號")]
        public string PartNumber { get; set; }

        [Display(Name = "件名")]
        public string PartName { get; set; }

        [Display(Name = "件名(英文)")]
        public string PartName_Eng { get; set; }

        [Display(Name = "Bom表材質")]
        public string Material { get; set; }

        [Display(Name = "線徑與板厚")]
        public string ThicknessWire { get; set; }

        [Display(Name = "途程代號(1)")]
        public string RoutingNo1 { get; set; }

        [Display(Name = "途程規範(1)")]
        public string RoutingRule1 { get; set; }

        [Display(Name = "途程代號(2)")]
        public string RoutingNo2 { get; set; }

        [Display(Name = "途程規範(2)")]
        public string RoutingRule2 { get; set; }

        [Display(Name = "途程代號(3)")]
        public string RoutingNo3 { get; set; }

        [Display(Name = "途程規範(3)")]
        public string RoutingRule3 { get; set; }

        [Display(Name = "途程代號(4)")]
        public string RoutingNo4 { get; set; }

        [Display(Name = "途程規範(4)")]
        public string RoutingRule4 { get; set; }

        [Required]
        [Display(Name = "新件/延用件")]
        public string NeworOld { get; set; }

        [Display(Name = "延用車型")] //有延用才需要填寫
        public string OldCarType { get; set; }

        [Required]
        [Display(Name = "來源")]
        public string Source { get; set; }

        [Required]
        [Display(Name = "數量")]
        public decimal Quantity { get; set; }

        [Display(Name = "類別")]
        public string Category { get; set; }

        [Display(Name = "備註")]
        public string Remark { get; set; }

        public Bom Bom { get; set; }

    }
}