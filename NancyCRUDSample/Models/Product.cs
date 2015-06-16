using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NancyCRUDSample.Models
{
    public class Product : Base
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public Product()
        { }
    }
}