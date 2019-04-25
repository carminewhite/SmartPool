using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPool.Models
{
    public class User : DataModel
    {
        [Key]
        public int Id {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
        public string PwHash {get; set;}
        public string Phone {get; set;}

        public List<Commute> Commutes {get; set;}
    }
}