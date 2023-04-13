using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Service.DTOs.Orders
{
    public  class OrderForCreationDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public decimal Longitute { get; set; }
        [Required]
        public decimal Latitute { get; set; }
        [Required]
        public IList<OrderDetailsDto> Details { get; set; }



    }
}
