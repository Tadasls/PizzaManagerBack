using System.ComponentModel.DataAnnotations;

namespace CompetitionEventsManager.Models.Dto.UserDTO
{
    /// <summary>
    /// DTO for Login
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// User UserName
        /// </summary>
        [Required(ErrorMessage = "Without Username not posible to Login")]
        [MaxLength(50, ErrorMessage = "Mark cannot be longer than 50 characters")]
        public string UserName { get; set; }
        /// <summary>
        /// UserPasswords place
        /// </summary>
        [Required(ErrorMessage = "Without Password not posible to Login")]
        [MaxLength(200, ErrorMessage = "Password cannot be longer than 200 characters")]
        public string Password { get; set; }
    }
}