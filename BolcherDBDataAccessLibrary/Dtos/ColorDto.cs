using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BolcherDBModelLibrary
{
    public partial class ColorDto
    {
        public ColorDto(Color color)
        {
            Id = color.Id;
            Name = color.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
