using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PriceQuationApi.Model
{
    //Data From PLM TESTTTTTTdsdgsdgsdgsdgsdg
    public class Oppo
    {
        [Key]
        [Display(Name ="Oppo編號")]
        public string OppoId { get; set; }

        [Required]
        [Display(Name = "狀態")]
        //1.Bom表匯入 , 2.量檢具需求填寫 3.夾治具需求填寫 , 
        //4.各單位報價  , 5.報價完成 , 6.作廢 , 7.獲得合約
        public int Status { get; set; }

        //OPPO 一對多 Bom
        public List<Bom> Boms { get; set; }
    }
}