﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wey.Domain
{
    public interface IEntity
    {
        Guid Key { get; set; }
    }
}
