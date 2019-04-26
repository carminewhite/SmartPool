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
        public int UserID {get;set;}
        public User User {get;set;}
    }
}