using CodingChallenge.Domain.Common;
using System;
using System.Collections.Generic;

namespace CodingChallenge.Domain.Entities
{
    public class Product : AuditableEntity, IHasDomainEvent
    {
        public Product()
        {
            this.DomainEvents = new List<DomainEvent>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public List<DomainEvent> DomainEvents { get; set; }
    }
}
