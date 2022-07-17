using System.ComponentModel.DataAnnotations;

namespace RecommendationSystem.Identity.DTO
{
    public class UserAuthCredentials
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
