using Microsoft.EntityFrameworkCore;
using Shopping.App.DAL;
using Shopping.App.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.App.Service
{
    public class OrderService
    {
        private readonly AppDbContext _dbContext;

        // pass data across pages
        public Order CachedProduct { get; set; }

        public OrderService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }


        public bool SubmitOrder(Order order)
        {
            using(var trans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Add(order);
                    _dbContext.AddRange(order.Details);
                    _dbContext.SaveChanges();
                    trans.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return false;
                    // throw new Exception($"Submit Order Failed {ex.Message}.");
                }
            }
            
        }

        public IQueryable<Order> GetOrders()
            => _dbContext.Orders
                    .Include(o => o.Details)
                    .ThenInclude(d => d.Product);

        public IList<Order> GetOrderByUserId(int userId)
            => GetOrders()
                    .Where(order => order.UserId == userId)
                    .ToList();

    }
}
