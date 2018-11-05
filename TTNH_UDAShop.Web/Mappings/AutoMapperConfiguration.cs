using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TTNH_UDAShop.Model.Models;
using TTNH_UDAShop.Web.Models;

namespace TTNH_UDAShop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Post, PostViewModel>();
                cfg.CreateMap<PostCategory, PostCategoryViewModel>();
                cfg.CreateMap<Tag, TagViewModel>();

                cfg.CreateMap<ProductCategory, ProductCategoryViewModel>();
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<ProductTag, ProductTagViewModel>();
                cfg.CreateMap<Footer, FooterViewModel>();
            });
        }
    }
}