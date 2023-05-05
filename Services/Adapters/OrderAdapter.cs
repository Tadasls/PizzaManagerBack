
using CompetitionEventsManager.Models;
using CompetitionEventsManager.Models.Dto.OrderDTO;

using CompetitionEventsManager.Services.Adapters.IAdapters;

namespace CompetitionEventsManager.Services.Adapters
{
    public class OrderAdapter : IOrderAdapter
    {
        public UpdateOrderDTO Bind(Order Order)
        {
            return new UpdateOrderDTO
            {
         
       
        };
        }

        public Order Bind(UpdateOrderDTO updateOrderDTO, int id)
        {
            return new Order
            {
        OrderID = id,

    
    };
        }



    









    }
}
