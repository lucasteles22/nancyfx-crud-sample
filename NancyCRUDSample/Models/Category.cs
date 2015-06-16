using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyCRUDSample.Models
{
    public class Category : Base
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public Category()
        {
            this.Products = new List<Product>();
        }
    }
}