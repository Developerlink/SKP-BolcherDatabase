﻿using BolcherDBModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BolcherDBModelLibrary.Interfaces
{
    public interface IColorRepository : IGenericRepository<Color>, ISearchable<Color>, IUniqueNameable
    {
    }
}
