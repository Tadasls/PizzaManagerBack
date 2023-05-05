namespace CompetitionEventsManager.Models.Dto.UserDTO
{
    /// <summary>
    /// DTO for login
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// UserUserName
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// User ID
        /// </summary>
        public int? ID { get; set; }
        /// <summary>
        /// User Role
        /// </summary>
        public string? Role { get; set; }
        /// <summary>
        /// User Token
        /// </summary>
        public string? Token { get; set; }
    }
}