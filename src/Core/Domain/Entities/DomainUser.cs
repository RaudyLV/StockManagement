using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public class DomainUser
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        
        public ICollection<Service> Services {get; set;} 
        public ICollection<Order> Orders {get; set;}
    }
}