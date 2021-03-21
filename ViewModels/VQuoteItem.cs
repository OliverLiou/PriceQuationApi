using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriceQuationApi.ViewModels
{
    public class VQuoteItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuoteItemId { get; set;}

        [Required]
        [Display(Name = "負責事項")]
        public string ResponsibleItem { get; set; }

        [Required]
        [Display(Name="負責單位")]
        public int DepartemntId {get; set;}

        public VDepartment VDepartment { get; set;}
    }
}