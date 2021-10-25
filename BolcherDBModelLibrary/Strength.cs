using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BolcherDBModelLibrary
{
    public partial class Strength
    {
        public Strength()
        {
            Candies = new HashSet<Candy>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot be more than 50 characters.")]
        public string Name { get; set; }

        public virtual ICollection<Candy> Candies { get; set; }
    }
}
