using System;
using System.ComponentModel.DataAnnotations;

namespace TimePunchClock.Models
{
	public class User
	{
		[Required]
		public Guid Id { get; set; }

		[Required, EmailAddress]
		public string Email { get; set; }

		[Required, MaxLength(20), MinLength(6)]
		public string Password { get; set; }

		[Required]
		public DateTime CreateAt { get; set; }

		[Required]
		public DateTime UpdateAt { get; set; }
    }
}

