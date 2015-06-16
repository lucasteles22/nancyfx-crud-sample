using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace NancyCRUDSample.Models.Context
{
    public interface IApplicationDbContext
    {
        IDbSet<Product> Products { get; set; }
        IDbSet<Category> Categories { get; set; }

        int SaveChanges();

        void SetModified(object entity);
    }
}
