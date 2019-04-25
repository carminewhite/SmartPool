using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPool.Models
{
    public class Ridership : DataModel
    {
        [Key]
        public int Id {get; set;}

        // Foreign Keys
        public int UserId {get; set;}
        public int CarpoolId {get; set;}

        // Navigation Properties
        public User user {get; set;}
        public Carpool carpool {get; set;}
    }
}