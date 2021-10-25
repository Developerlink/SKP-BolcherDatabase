using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BolcherDBModelLibrary
{
    [Index(IsUnique = true, Name = nameof(Name))]
    public partial class Candy
    {
        public Candy()
        {
            OrderLines = new HashSet<OrderLine>();
        }

        [Key]
        public int Id { get; set; }
        public int ColorId { get; set; }
        public int SournessId { get; set; }
        public int StrengthId { get; set; }
        public int FlavourId { get; set; }
        public string Name { get; set; }
        public short Weight { get; set; }
        public short ProductionCost { get; set; }

        public virtual Color Color { get; set; }
        public virtual Flavour Flavour { get; set; }
        public virtual Sourness Sourness { get; set; }
        public virtual Strength Strength { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
