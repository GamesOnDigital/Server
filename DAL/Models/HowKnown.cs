﻿using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class HowKnown
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
