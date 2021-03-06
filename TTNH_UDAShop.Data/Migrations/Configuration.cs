﻿namespace TTNH_UDAShop.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TTNH_UDAShop.Model.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TTNH_UDAShop.Data.TTNH_UDAShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TTNH_UDAShop.Data.TTNH_UDAShopDbContext context)
        {
            CreateProductCategorySample(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new TTNH_UDAShopDbContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new TTNH_UDAShopDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "tuan",
            //    Email = "tuan.international@gmail.com",
            //    EmailConfirmed = true,
            //    BirthDay = DateTime.Now,
            //    FullName = "Nguyen Anh Tuan"

            //};

            //manager.Create(user, "123654$");

            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("tuan.international@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });


        }

        private void CreateProductCategorySample(TTNH_UDAShop.Data.TTNH_UDAShopDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
            {
                new ProductCategory() { Name="Điện lạnh",Alias="dien-lanh",Status=true },
                 new ProductCategory() { Name="Viễn thông",Alias="vien-thong",Status=true },
                  new ProductCategory() { Name="Đồ gia dụng",Alias="do-gia-dung",Status=true },
                   new ProductCategory() { Name="Mỹ phẩm",Alias="my-pham",Status=true }
            };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }

        }
    }
}
