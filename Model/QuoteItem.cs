using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PriceQuationApi.Model
{
    public class QuoteItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuoteItemId { get; set;}

        [Required]
        [Display(Name = "負責事項")]
        public string ResponsibleItem { get; set; }

        [Required]
        [Display(Name="負責單位")]
        public string DepartemntId {get; set;}

        public Department Department { get; set;}
    }
}