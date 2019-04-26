using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPool.Models
{
    public class ViewPools
    {
        public List<Commute> AllCommutes {get; set;}
        public Commute ClickedCommute {get; set;}
    }
}