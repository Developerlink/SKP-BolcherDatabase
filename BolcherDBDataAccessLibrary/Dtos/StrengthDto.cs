using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BolcherDBModelLibrary
{
    public partial class StrengthDto
    {
        public StrengthDto(Strength strength)
        {
            Id = strength.Id;
            Name = strength.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
