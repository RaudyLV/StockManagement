using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string ClientId  { get; set; }
        public DomainUser Client { get; set; }

        public Guid ServiceId { get; set; }
        public Service Service { get; set; }

        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
    }
}

