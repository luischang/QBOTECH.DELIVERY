using System;
using System.Collections.Generic;

namespace QBOTECH.DELIVERY.CORE.Entities;

public partial class SubscriptionPlans
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int DeliveryLimit { get; set; }
}
