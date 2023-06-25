using System;
using System.ComponentModel.DataAnnotations;

namespace TimePunchClock.Models
{
	public class CreateUser
	{
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordConfirm { get; set; }
    }
}

