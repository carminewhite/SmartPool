using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPool.Models
{
    public class Carpool : DataModel
    {
        [Key]
        public int Id {get; set;}
        
        [NotMapped]
        public Commute MondayMerge {get; set;}
        [NotMapped]
        public Commute TuesdayMerge {get; set;}
        [NotMapped]
        public Commute WednesdayMerge {get; set;}
        [NotMapped]
        public Commute ThursdayMerge {get; set;}
        [NotMapped]
        public Commute FridayMerge {get; set;}
        [NotMapped]
        public Commute SaturdayMerge {get; set;}
        [NotMapped]
        public Commute SundayMerge {get; set;}


        public List<Commute> Commutes {get; set;}
    }
}