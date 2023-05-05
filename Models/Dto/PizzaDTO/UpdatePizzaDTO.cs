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
    public class UpdatePizzaDTO
    {
      
        public string? PizzaName { get; set; }


    }
}
