using System.ComponentModel.DataAnnotations;

namespace CompetitionEventsManager.Models.Dto.UserDTO
{
    /// <summary>
    /// DTO for register
    /// </summary>
    public class RegisterUserRequest
    {
        /// <summary>
        /// User UserName
        /// </summary>
        [Required(ErrorMessage = "Very Important")]
        [MaxLength(50, ErrorMessage = "Mark cannot be longer than 50 characters")]
        public string? UserName { get; set; }
        /// <summary>
        /// User FirstName
        /// </summary>
        [Required]
        [MaxLength(100, ErrorMessage = "cannot be longer than 100 characters")]
        public string? FirstName { get; set; }
        /// <summary>
        /// User LastName
        /// </summary>
        [Required]
        [MaxLength(100, ErrorMessage = "cannot be longer than 100 characters")]
        public string? LastName { get; set; }
        /// <summary>
        /// User Password
        /// </summary>
        [Required]
        [MaxLength(200, ErrorMessage = "Password cannot be longer than 200 characters")]
        public string? Password { get; set; }
        /// <summary>
        /// User Role
        /// </summary>
        [Required]
        [MaxLength(50, ErrorMessage = "Role cannot be longer than 50 characters")]
        public string? Role { get; set; }
    
        /// <summary>
        /// User phone
        /// </summary>
        [MaxLength(100, ErrorMessage = "cannot be longer than 100 characters")]
        public string? Phone { get; set; }
        /// <summary>
        /// user email
        /// </summary>
        [MaxLength(100, ErrorMessage = "cannot be longer than 100 characters")]
        public string? Email { get; set; }
  




    }
}



