using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public int? GenderId { get; set; }

    public int? City { get; set; }

    public int? HowKnownId { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public virtual Gender? Gender { get; set; }

    public virtual HowKnown? HowKnown { get; set; }
}
