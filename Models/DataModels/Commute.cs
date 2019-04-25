using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPool.Models
{
    public class Commute : DataModel
    {
        [Key]
        public int Id {get; set;}
        public DateTime ArriveBy {get; set;}
        public DayOfWeek Day {get; set;}

        // Foreign Keys
        public int StartLocationId {get; set;}
        public int EndLocationId {get; set;}
        public int CarpoolId {get; set;}

        // Navigation Properties
        [ForeignKey("StartLocationId")]
        [InverseProperty("commutesStartingHere")]
        public Location startLocation {get; set;}

        [ForeignKey("EndLocationId")]
        [InverseProperty("commutesEndingHere")]
        public Location endLocation {get; set;}

        [ForeignKey("CarpoolId")]
        [InverseProperty("commutes")]
        public Carpool carpool {get; set;}
    }
}