using CompetitionEventsManager.Models;
using CompetitionEventsManager.Models.Dto.HorseDTO;

namespace CompetitionEventsManager.Services.Adapters.IAdapters
{
    public interface IPizzaAdapter
    {
        UpdatePizzaDTO Bind(Pizza Pizza);
        Pizza Bind(UpdatePizzaDTO updatePizzaDTO, int id);
    }
}