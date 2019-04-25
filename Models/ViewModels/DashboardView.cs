using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPool.Models
{
    public class DashboardView
    {
        public User user {get; set;}
        public CreateCarpool createCarpool {get; set;}
    }
}