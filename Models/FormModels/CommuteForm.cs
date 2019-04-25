using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartPool.Models
{
    public class CommuteForm : DataModel
    {
        public User user {get; set;}
        public Carpool carpool {get; set;}

        [Required]
        public string StartPt {get; set;}
        public string EndCity {get; set;}
        [Required]
        public string EndPt {get; set;}
        [Required]
        public DateTime ArriveBy {get; set;}
        [Required]
        public DayOfWeek Day {get; set;}

        public bool Monday {get;set;}
        public bool Tuesday {get;set;}
        public bool Wednesday {get;set;}
        public bool Thurday {get; set;}
        public bool Friday {get; set;}
        public bool Saturday {get; set;}
        public bool Sunday {get;set;}

    }
}