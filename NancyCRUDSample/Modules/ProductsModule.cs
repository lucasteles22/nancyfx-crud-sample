using Nancy;
using NancyCRUDSample.Models;
using NancyCRUDSample.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.ModelBinding;

namespace NancyCRUDSample.Modules
{
    public class ProductsModule : NancyModule
    {
        public ProductsModule(IApplicationDbContext ctx)
        {
            Get["/products"] = _ =>
                {
                    return View["index", ctx.Products.ToList()];
                };

            Get["/product/new"] = _ =>
                {
                    ViewBag.Categories = ctx.Categories.OrderBy(x => x.Name).ToList();
                    return View["new", new Product()];
                };

            Post["/product/new"] = _ =>
                {
                    var product = this.Bind<Product>();
                    if (product != null)
                    {
                        ctx.Products.Add(product);
                        ctx.SaveChanges();
                        return Response.AsRedirect("/products");
                    }
                    return 500;
                };

            Get["/product/update/{id:guid}"] = _ =>
                {
                    var id = new Guid(_.id);
                    var product = ctx.Products.Where(x => x.Id == id).FirstOrDefault();
                    if (product != null)
                    {
                        ViewBag.Categories = ctx.Categories.OrderBy(x => x.Name).ToList();
                        return View["update", new Product() { Id = product.Id, Name = product.Name, CategoryId = product.CategoryId }];
                    }
                    return 404;
                };

            Post["/product/update"] = _ =>
                {
                    var product = this.Bind<Product>();
                    if (product != null)
                    {
                        ctx.SetModified(product);
                        ctx.SaveChanges();
                        return Response.AsRedirect("/products");
                    }
                    return 500;
                };


            Get["/product/delete/{id:guid}"] = _ =>
            {
                var id = new Guid(_.id);
                if (ctx.Products.Any(x => x.Id == id))
                {
                    ViewBag.ProductId = id;
                    return View["delete"];
                }
                return 404;
            };

            Post["/product/deleteconfirmed"] = _ =>
            {
                Product product = this.Bind<Product>();
                if (product != null)
                {
                    var dbCategory = ctx.Products.Where(x => x.Id == product.Id).FirstOrDefault();
                    ctx.Products.Remove(dbCategory);
                    ctx.SaveChanges();
                    return Response.AsRedirect("/products");
                }
                return 500;
            };
        }
    }
}