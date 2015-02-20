using System;
using RSR.Data.Models;

namespace RSR.Data.Repositories.Contracts
{
	public interface IUserRepository : IMassiveRepositoryBase<User, int>
	{
		User GetByUserName(string userName);
		User GetByUniqueId(Guid uniqueId);
		dynamic GetEntityByUserName(string userName);
	}
}
