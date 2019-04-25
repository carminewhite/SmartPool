using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPool.Models
{
    public class CreateCarpool
    {
        [Required]
        public string Name {get; set;}
    }
}