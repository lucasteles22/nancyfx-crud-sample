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
    public class CategoriesModule : NancyModule
    {
        public CategoriesModule(IApplicationDbContext ctx)
        {
            Get["/categories"] = _ =>
                {
                    var categories = ctx.Categories.ToList();
                    return View["index", categories];
                };

            Get["/category/new"] = _ =>
                {
                    var category = new Category();
                    return View["new", category];
                };

            Post["/category/new"] = parameters =>
                {
                    var category = this.Bind<Category>();
                    if (category != null)
                    {
                        ctx.Categories.Add(category);
                        ctx.SaveChanges();
                        return Response.AsRedirect("/categories");
                    }
                    return 500;                   
                };

            Get["/category/update/{id:guid}"] = _ =>
                {
                    var id = new Guid(_.id);
                    var category = ctx.Categories.Where(x => x.Id == id).FirstOrDefault();
                    if(category != null)
                    {
                        return View["update", new Category() { Name = category.Name, Id = category.Id}];
                    }
                    return 404;
                };

            Post["/category/update"] = parameters =>
                {
                    Category category = this.Bind<Category>();
                    if(category != null)
                    {
                        ctx.SetModified(category);
                        ctx.SaveChanges();
                        return Response.AsRedirect("/categories");
                    }
                    return 404;
                };
            Get["/category/delete/{id:guid}"] = _ =>
                {
                    var id = new Guid(_.id);
                    if (ctx.Categories.Any(x => x.Id == id))
                    {
                        ViewBag.CategoryId = id;
                        return View["delete"];
                    }
                    return 404;
                };

            Post["/category/deleteconfirmed"] = _ =>
            {
                Category category = this.Bind<Category>();
                if (category != null)
                {
                    var dbCategory = ctx.Categories.Where(x => x.Id == category.Id).FirstOrDefault();
                    ctx.Categories.Remove(dbCategory);
                    ctx.SaveChanges();
                    return Response.AsRedirect("/categories");
                }
                return 404;
            };
        }
    }
}