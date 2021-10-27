using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BolcherDBModelLibrary
{
    public partial class SournessDto
    {
        public SournessDto(Sourness sourness)
        {
            Id = sourness.Id;
            Name = sourness.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
