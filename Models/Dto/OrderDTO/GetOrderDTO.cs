using System.ComponentModel.DataAnnotations;

namespace CompetitionEventsManager.Models.Dto.OrderDTO
{
    public class GetOrderDTO
    {
        public GetOrderDTO(Order Order)
        {
            OrderID = Order.OrderID;
            TotalAmount = Order.TotalAmount;
            OrderDate = Order.OrderDate;

        }

        public int OrderID { get; set; }

        public decimal? TotalAmount { get; set; }

        public DateTime? OrderDate { get; set; }


    }
}