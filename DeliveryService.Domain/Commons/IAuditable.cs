using DeliveryService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Domain.Commons
{
    internal interface IAuditable
    {
        Guid Id { get; set; }
        DateTime CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
        Guid? UpdateBy { get; set; }
        ItemState State { get; set; }

       

    }
}
