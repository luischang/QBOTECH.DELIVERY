using System;
using System.Collections.Generic;

namespace QBOTECH.DELIVERY.CORE.Entities;

public partial class Users
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string CountryCode { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    public string Type { get; set; } = null!;
}
