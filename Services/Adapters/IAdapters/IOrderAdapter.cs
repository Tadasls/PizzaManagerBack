using CompetitionEventsManager.Models;
using CompetitionEventsManager.Models.Dto.OrderDTO;

namespace CompetitionEventsManager.Services.Adapters.IAdapters
{
    public interface IOrderAdapter
    {
        UpdateOrderDTO Bind(Order Order);
        Order Bind(UpdateOrderDTO updateOrderDTO, int id);
    }
}