﻿namespace Domain.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }
    }
}
