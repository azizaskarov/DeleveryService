using DeliveryService.Domain.Commons;
using DeliveryService.Domain.Entities.Orders;
using DeliveryService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Domain.Entities.Users
{
    public class User : IAuditable
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }
        public ItemState State { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public User()
        {
            Orders = new List<Order>();

        }

        public void Create()
        {
            CreateDate = DateTime.Now;
            State = ItemState.Created;
        }
        public void Update()
        {
            UpdateDate = DateTime.Now;
            State=ItemState.Updated;
        }
        public void Delete()
        {
            State = ItemState.Deleted;
        }
    }
}
