using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.FileSystemGlobbing;
using System.Drawing;
using Microsoft.Extensions.Hosting;

namespace CompetitionEventsManager.Models.Dto.HorseDTO
{
    /// <summary>
    /// DTO
    /// </summary>
    public class GetPizzaDTO
    {
        public GetPizzaDTO(Pizza pizza)
        {
            PizzaID = pizza.PizzaID;
            PizzaName = pizza.PizzaName;
            UserId = pizza.UserId;
        }

        public int PizzaID { get; set; }
        public string PizzaName { get; set; }
        public int? UserId { get; set; }
    }
}
