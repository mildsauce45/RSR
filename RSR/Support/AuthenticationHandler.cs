using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;
using RSR.Data.Models;
using RSR.Data.Repositories.Contracts;
using RSR.Support.Contracts;

namespace RSR.Support
{
	public class AuthenticationHandler : IAuthenticationHandler, IUserMapper
	{
		private readonly IUserRepository userRepository;

		public AuthenticationHandler(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		public Guid? ValidateUser(string userName, string password)
		{
			var user = userRepository.GetEntityByUserName(userName);

			if (user == null)
				return null;

			//password = ((string)(password + user.UniqueId.ToString())).ToMd5();

			return (string)user.Password == password ? user.UniqueId : null;
		}

		public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
		{
			var user = userRepository.GetByUniqueId(identifier);

			return new UserIdentity(user);
		}

		public User CreateNewUser(User user)
		{
			var existingUser = userRepository.GetByUserName(user.UserName);

			if (existingUser != null)
				return null;

			var id = userRepository.Add(user);

			return userRepository.Get(id);
		}
	}
}