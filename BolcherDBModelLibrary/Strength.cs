using System;
using System.Collections.Generic;

#nullable disable

namespace BolcherDBModelLibrary
{
    public partial class Strength
    {
        public Strength()
        {
            Candies = new HashSet<Candy>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Candy> Candies { get; set; }
    }
}
