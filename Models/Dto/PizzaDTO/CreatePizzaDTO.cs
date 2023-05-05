using System.ComponentModel.DataAnnotations;

namespace CompetitionEventsManager.Models.Dto.HorseDTO
{
    /// <summary>
    /// DTO With Pizza data
    /// </summary>
    public class CreatePizzaDTO
    {
       /// <summary>
       /// Pizza name
       /// </summary>
        public string PizzaName { get; set; }
        public string? Size { get; set; }
        public int? Toppings { get; set; }
        public decimal? Price { get; set; }

    }
}