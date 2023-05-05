using CompetitionEventsManager.Models;

namespace CompetitionEventsManager.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
       
        IEnumerable<Order> Getdata_With_EagerLoading();


        IEnumerable<Order> GetSomeWithSQL(int userId);

    }
}