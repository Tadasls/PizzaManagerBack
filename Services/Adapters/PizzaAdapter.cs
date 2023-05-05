using CompetitionEventsManager.Models;
using CompetitionEventsManager.Models.Dto.HorseDTO;
using CompetitionEventsManager.Services.Adapters.IAdapters;
using Microsoft.Extensions.Hosting;
using System.Drawing;

namespace CompetitionEventsManager.Services.Adapters
{
    public class PizzaAdapter  : IPizzaAdapter
    {
        public UpdatePizzaDTO Bind(Pizza Pizza)
        {
            return new UpdatePizzaDTO
            {
             
            };
        }

        public Pizza Bind(UpdatePizzaDTO updatePizzaDTO, int id)
        {
            return new Pizza
            {
               
                PizzaID = id
            };
        }






    }
}
