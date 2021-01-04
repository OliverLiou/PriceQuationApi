using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PriceQuationApi.Model
{
    public class QuoteDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuoteDetailId {get; set;}

        [Required]
        [Display(Name = "總成件號")]
        public string AssemblyPartNumber {get; set;}

        [Required]
        [Display(Name = "報價細項")]
        public int QuoteItemId { get; set; }

        [Required]
        [Display(Name = "報價截止時間(單位)")]
        [Column(TypeName = "Date")]
        public DateTime QuoteTime { get; set; }

        [Display(Name = "報價人員")]
        public int? UserId { get; set; }

        [Display(Name = "實際完成時間(單位)")]
        public DateTime? FinishedTime { get; set; }

        //[一對多] Bom
        public Bom Bom {get; set;}

    }
}