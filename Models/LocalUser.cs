using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompetitionEventsManager.Models
{
    /// <summary>
    /// Local User
    /// </summary>
    public class LocalUser
    {
        
        /// <summary>
        /// User ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
   
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }
        //[MaxLength(100)]
        //public string FirstName { get; set; }
        //[MaxLength(100)]
        //public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [MaxLength(50)]
        public string Role { get; set; }
        public DateTime RegistrationDate { get; set; }
  
        //[Display(Name = "Phone")]
        //[MaxLength(50)]
        //public string? Phone { get; set; }
        [Display(Name = "Email")]
        public string? Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Pizza> Pizzas { get; set; }



                
               


    }
}
