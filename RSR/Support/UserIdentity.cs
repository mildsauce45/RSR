using System.Collections.Generic;
using Nancy.Security;
using RSR.Data.Models;

namespace RSR.Support
{
	public class UserIdentity : IUserIdentity
	{
		public string UserName { get; set; }
		public IEnumerable<string> Claims { get; set; }
		public int UserId { get; set; }

		public UserIdentity(User user)
		{
			this.UserName = user.UserName;
			this.UserId = user.Id;
		}
	}
}