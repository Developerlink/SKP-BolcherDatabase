using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BolcherDBModelLibrary
{
    public partial class FlavourDto
    {
        public FlavourDto(Flavour flavour)
        {
            Id = flavour.Id;
            Name = flavour.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

    }
}