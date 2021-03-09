using System.ComponentModel.DataAnnotations;

namespace PriceQuationApi.Model
{
    public class FixtureItem
    {
        [Key]
        [Display(Name = "編號")]
        public string FixtureItemId { get; set; }

        [Required]
        [Display(Name = "總成件號")]
        public string AssemblyPartNumber { get; set; }

        [Required]
        [Display(Name = "件號")]
        public string PartNumber { get; set; }

        [Display(Name = "工程名稱")]
        public string EngineeringName { get; set; }

        [Display(Name = "組立工序")]
        public string EngineeringOrder { get; set; }

        [Display(Name = "是否共用")]
        public bool? Share { get; set; }

        [Display(Name = "需要夾治具")]
        public bool? NeedFixture { get; set; }

        [Display(Name = "夾治具名稱")]
        public string FixtureName { get; set; }

        [Display(Name = "夾治具套數")]
        public decimal? FixtureQuantity { get; set; }

        [Display(Name = "報價單位")]
        public int? DepartemntId { get; set; }

        [Display(Name = "夾治具備註")]
        public string FixtureRemark { get; set; }

        [Display(Name = "夾治具單價")]
        public decimal? FixtureUnitPrice { get; set; }

        [Display(Name = "夾治具總金額")]
        public decimal? FixtureTotal { get; set; }

        [Display(Name = "需要設備")]
        public bool? NeedEquipment { get; set; }

        [Display(Name = "設備名稱")]
        public string EquipmentName { get; set; }

        [Display(Name = "設備數量")]
        public decimal? EquipmentQuantity { get; set; }

        [Display(Name = "設備單價")]
        public decimal? EquipmentUnitPrice { get; set; }

        [Display(Name = "設備總金額")]
        public decimal? EquipmentTotal { get; set; }

        [Display(Name = "設備備註")]
        public string EquipmentRemark { get; set; }

        public Bom Bom { get; set; }
    }
}