using System.ComponentModel.DataAnnotations;

namespace CompetitionEventsManager.Models.Dto.UserDTO
{
    /// <summary>
    /// Get DTO
    /// </summary>
    public class GetUserDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///Users name
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Users First Name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Users Last Name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Users  Role
        /// </summary>
        public string Role { get; set; }

    }
}

