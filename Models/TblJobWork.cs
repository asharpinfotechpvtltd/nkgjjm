﻿using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Nkgjjm.Models
{
    public class JobWork
    {
        [Key]
        public int Sno { get; set; }
        public string WorkorderId { get; set; }
        public int District { get; set; }
        public int Block { get; set; }
        public int GramPanchaayat { get; set; }
        public Int64 VillageCode { get; set; }
        public int ContractorId { get; set; }        
        public int JobWorkCategory { get; set; }        
        public string Date { get; set; }       
        public bool IsBomGenerated { get; set; }
        public bool Iscompleted { get; set; }
        public int Warehouseid { get; set; }


    }
}
