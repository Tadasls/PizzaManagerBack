using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CompetitionEventsManager.Models
{
    /// <summary>
    /// Order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Order ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MaxLength(20)]
        public int OrderID { get; set; }


        [MaxLength(20)]
        public int? UserId { get; set; }
        public virtual LocalUser? LocalUser { get; set; }
        public virtual List<Pizza> Orders { get; set; }

        public decimal? TotalAmount { get; set; }

        public DateTime? OrderDate { get; set; }



    }
} 