﻿using System.ComponentModel.DataAnnotations;

namespace RecommendationSystem.Identity.DTO
{
    public class UserRegisterCredentials
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "User name is required.")]
        public string UserName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
