using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompetitionEventsManager.Models.Dto.OrderDTO
{
    public class CreateOrderDTO
    {

        public List<Pizza>? Orders { get; set; }
        public decimal? TotalAmount { get; set; }

    }
}