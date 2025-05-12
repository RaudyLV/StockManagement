using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } // 1 to 5

        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public string ReviewerId { get; set; }
        public DomainUser Reviewer { get; set; }
    }
}