using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

#nullable disable

namespace BolcherDBModelLibrary
{
    public partial class CandyDto
    {
        public CandyDto(Candy candy)
        {
            Id = candy.Id;
            ColorId = candy.ColorId;
            SournessId = candy.SournessId;
            StrengthId = candy.StrengthId;
            FlavourId = candy.FlavourId;
            Name = candy.Name;
            Weight = candy.Weight;
            ProductionCost = candy.ProductionCost;
            if (candy.OrderLines != null && candy.OrderLines.Any())
            {
                Amount = candy.OrderLines.Where(o => o.CandyId == candy.Id).Select(o => o.Amount).FirstOrDefault();
            }
            Color = new ColorDto(candy.Color);
            Flavour = new FlavourDto(candy.Flavour);
            Sourness = new SournessDto(candy.Sourness);
            Strength = new StrengthDto(candy.Strength);
        }

        public int Id { get; set; }
        public int ColorId { get; set; }
        public int SournessId { get; set; }
        public int StrengthId { get; set; }
        public int FlavourId { get; set; }
        public string Name { get; set; }
        public short Weight { get; set; }
        public short ProductionCost { get; set; }
        public short Amount { get; set; }

        public virtual ColorDto Color { get; set; }
        public virtual FlavourDto Flavour { get; set; }
        public virtual SournessDto Sourness { get; set; }
        public virtual StrengthDto Strength { get; set; }
    }
}
