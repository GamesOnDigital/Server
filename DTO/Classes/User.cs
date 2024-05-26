using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Classes
{
    public class User
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

    }
}
