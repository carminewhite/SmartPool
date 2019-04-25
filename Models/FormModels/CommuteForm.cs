using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartPool.Models
{
    public class CommuteForm
    {
        [Required]
        public int StartLocationId {get; set;}
        [Required]
        public int EndLocationId {get; set;}
        [Required]
        public int CarpoolId {get; set;}
        [Required]
        public DateTime ArriveBy {get; set;}
        public bool Monday {get;set;}
        public bool Tuesday {get;set;}
        public bool Wednesday {get;set;}
        public bool Thursday {get; set;}
        public bool Friday {get; set;}
        public bool Saturday {get; set;}
        public bool Sunday {get;set;}

    }
}