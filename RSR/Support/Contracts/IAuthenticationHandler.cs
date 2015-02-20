using System;
using RSR.Data.Models;

namespace RSR.Support.Contracts
{
	public interface IAuthenticationHandler
	{
		Guid? ValidateUser(string userName, string password);
		User CreateNewUser(User newUser);
	}
}