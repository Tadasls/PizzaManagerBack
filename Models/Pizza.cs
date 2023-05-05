using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompetitionEventsManager.Models
{
    /// <summary>
    /// Pizza Model
    /// </summary>
    public class Pizza
    {
        /// <summary>
        /// Pizza ID
        /// </summary>     
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PizzaID { get; set; }

        [Required]
        [MaxLength(100)]
        public string PizzaName { get; set; }
        public string? Size { get; set; }
        public int? Toppings { get; set; }
        public decimal? Price { get; set; }
        public int? UserId { get; set; }

        public virtual LocalUser? LocalUser { get; set; }
        public virtual Order? Order { get; set; }


    }
}
