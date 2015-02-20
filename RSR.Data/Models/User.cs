using System;

namespace RSR.Data.Models
{
	public class User
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public Guid UniqueId { get; set; }

		/// <summary>
		/// Only used when adding a new user
		/// </summary>
		public string Password { get; set; }
	}
}
