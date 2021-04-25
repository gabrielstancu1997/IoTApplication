using System.ComponentModel.DataAnnotations;

namespace IoTApplication.Models
{
    public class AuthenticationRequest
    {
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
