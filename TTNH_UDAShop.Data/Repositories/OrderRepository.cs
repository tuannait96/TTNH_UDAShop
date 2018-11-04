using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using TTNH_UDAShop.Data.Infrastructure;
using TTNH_UDAShop.Model.Models;

namespace TTNH_UDAShop.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        
    }
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}