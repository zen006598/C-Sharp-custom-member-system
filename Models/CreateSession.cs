using System;
using System.ComponentModel.DataAnnotations;

namespace TimePunchClock.Models
{
	public class CreateSession
	{
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
	}
}

