using System;
using Microsoft.EntityFrameworkCore;
using TimePunchClock.Models;
namespace TimePunchClock.Data
{
	public class UserContext : DbContext
	{
		public UserContext(DbContextOptions<UserContext> options) : base(options){ }
		public DbSet<User> User { get; set; }
	}
}

