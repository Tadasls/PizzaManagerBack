using CompetitionEventsManager.Data;
using CompetitionEventsManager.Models;
using CompetitionEventsManager.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CompetitionEventsManager.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly DBContext _db;
      

        public OrderRepository(DBContext db) : base(db)
        {
            _db = db;
           
        }



        public IEnumerable<Order> GetSomeWithSQL(int userId)
        {
           
            var entities =
                from Order in _db.Orders.Where(x => x.UserId == userId)
                where Order.UserId == userId
                select Order;

            return entities; 
        }
        public IEnumerable<Order> Getdata_With_EagerLoading()
        {
           
            var duomenys = _db.Orders
            .Include(e => e.Orders)
            .ToList();

            return duomenys; 
           
        }




    }
}
