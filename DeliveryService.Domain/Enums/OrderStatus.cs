﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Domain.Enums
{
    public enum OrderStatus
    {
        Ordered = 1,
        Confirmed,
        Cooked,
        Delivered,
        Recieved

    }
}
