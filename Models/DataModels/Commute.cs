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
<<<<<<< HEAD
        public User user {get; set;}
        public Carpool carpool {get; set;}
        public string StartPt {get; set;}
        public string EndCity {get; set;}
        public string EndPt {get; set;}

=======
>>>>>>> 515cf0b4be585ec6a23e142d9f3bf3739bfb4429
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

        public Carpool carpool {get; set;}
    }
}