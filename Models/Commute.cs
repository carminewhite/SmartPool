using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartPool.Models
{
    public class Commute
    {
       [Key]
       public int Id {get; set;}
        public User user {get; set;}
        public Carpool carpool {get; set;}

        public string StartPt {get; set;}
        public string EndCity {get; set;}
        public string EndPt {get; set;}

        public DateTime ArriveBy {get; set;}
        public DayOfWeek Day {get; set;}

    }
}