using DeliveryService.Domain.Commons;
using DeliveryService.Domain.Entities.Users;
using DeliveryService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeliveryService.Domain.Entities.Orders
{
    public  class Order : IAuditable
    {
        public Guid Id { get; set; }
        public decimal Longitute { get; set; }  
        public decimal Latitute { get; set; }
        public OrderStatus Status { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }
        public ItemState State { get; set; }

        [ForeignKey(nameof(User))]

        public Guid UserId { get; set; }
        [JsonIgnore]

        public virtual User user { get; set; }
        
        public virtual  ICollection<OrderDetails> OrderDetails { get; set; }
        public object User { get; set; }

        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }

        public void Create()
        {
            CreateDate = DateTime.Now;
            State = ItemState.Created;
        }
        public void Update()
        {
            UpdateDate = DateTime.Now;
            State = ItemState.Updated;
        }
        public void Delete()
        {
            State = ItemState.Deleted;
        }
    }
}
