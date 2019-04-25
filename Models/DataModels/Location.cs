using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPool.Models
{
    public class Location : DataModel
    {
        [Key]
        public int Id {get;set;}
        public string LocationNickname {get;set;}
        public string Address {get;set;}
        public string City {get;set;}
        public string State {get;set;}
        public int Zip {get;set;}
<<<<<<< HEAD
        public int UserID {get;set;}
        public User User {get;set;}
=======

        // Foreign Keys
        public int UserId {get;set;}

        // Navigation Properties
        public User user {get;set;}
        public List<Commute> commutesStartingHere {get; set;}
        public List<Commute> commutesEndingHere {get; set;}
>>>>>>> 515cf0b4be585ec6a23e142d9f3bf3739bfb4429
    }
}