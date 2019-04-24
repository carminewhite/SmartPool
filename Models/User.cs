using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartPool.Models
{
    public class User
    {
        [Key]
        public int Id {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}

        public string Email {get; set;}
        public string PwHash {get; set;}

        public string Phone {get; set;}

        public List<Commute> Commutes {get; set;}

        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}

        public User()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}