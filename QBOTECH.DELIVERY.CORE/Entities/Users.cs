using System;
using System.Collections.Generic;

namespace QBOTECH.DELIVERY.CORE.Entities;

public partial class Users
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int? SubscriptionPlanId { get; set; }
}
