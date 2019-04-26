using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPool.Models
{
    public class UpdateCarpool : DataModel
    {
        [Required]
        public string Name {get; set;}

        [Required]
        public string Description {get; set;} = null;

        // Foreign Keys
        public int UserId {get; set;}

        // Navigation Properties
        public User user {get; set;}
        public List<Commute> commutes {get; set;}
        public List<Ridership> riderships {get; set;}
    }
}