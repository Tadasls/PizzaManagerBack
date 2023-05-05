using System.ComponentModel.DataAnnotations;

namespace CompetitionEventsManager.Models.Dto.OrderDTO
{
    public class FilterOrderRequest
    {
        /// <summary>
        /// paieska pagal Order 
        /// </summary>
        [MaxLength(100)]
        public int? OrderID { get; set; }



    }
}