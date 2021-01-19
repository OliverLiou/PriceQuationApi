using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriceQuationApi.Model
{
    public class MeasuringItem
    {
        [Key, ForeignKey("BomItem")]
        public string BomItemId { get; set; }

        [Display(Name = "需要量檢具")]
        public bool NeedMeausring { get; set; }

        [Display(Name = "數量")]
        public decimal Quantity { get; set; }

        [Display(Name = "量檢具名稱")]
        public string MeasuringName { get; set; }

        [Display(Name = "量檢具備註")]
        public string MeasuringRemark { get; set; }

        [Display(Name = "報價單位")]
        public int DepartemntId { get; set; }

        [Display(Name = "量檢具單價")]
        public decimal MeasuringUnitFee { get; set; }

        [Display(Name = "量檢具總計")]
        public decimal MeasuringTotal { get; set; }

        [Display(Name = "量檢具費用備註")]
        public string MeasuringTotalRemark { get; set; }

        public BomItem BomItem { get; set; }
    }
}