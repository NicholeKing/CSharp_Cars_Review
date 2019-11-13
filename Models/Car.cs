using System;
using System.ComponentModel.DataAnnotations;

namespace Cars.Models
{
    public class Car
    {
        [Key]
        public int CarId {get;set;}

        [Required(ErrorMessage="Make is required")]
        public string Make {get;set;}

        [Required(ErrorMessage="Model is required")]
        public string Model {get;set;}

        [Required(ErrorMessage="Year is required")]
        public int Year {get;set;}

        [Required(ErrorMessage="Color is required")]
        public string Color {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        public int UserId {get;set;}
        public User Owner {get;set;}
    }
}