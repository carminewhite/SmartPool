using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPool.Models
{
    public class FormLocation : DataModel
    {
        [Required]
        public string LocationNickname {get;set;}
        
        [Required]
        public string Address {get;set;}

        [Required]
        public string City {get;set;}

        [Required]
        public string State {get;set;}

        [Required]
        [Range(10000,99999, ErrorMessage="Must be 5 characters")]
        public int Zip {get;set;}
    }
}