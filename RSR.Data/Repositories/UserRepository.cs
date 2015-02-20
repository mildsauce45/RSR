using System;
using System.Linq;
using RSR.Data.Models;
using RSR.Data.Repositories.Contracts;

namespace RSR.Data.Repositories
{
	public class UserRepository : MassiveRespositoryBase<User, int>, IUserRepository
	{
		public UserRepository()
			: base("Reader", "Users", "Id")
		{
		}

		public User GetByUserName(string userName)
		{
			return this.Find("UserName = @0", args: userName).FirstOrDefault();
		}

		public User GetByUniqueId(Guid uniqueId)
		{
			return this.Find("UniqueId = @0", args: uniqueId).FirstOrDefault();
		}

		public dynamic GetEntityByUserName(string userName)
		{
			return this.GetDynamicSet().Query("select * from Users where Username = @0", userName).FirstOrDefault();
		}

		public override User ToConceptualModel(dynamic row)
		{
			var user = new User();

			user.Id = row.Id;
			user.UserName = row.UserName;
			user.Email = row.Email;
			user.UniqueId = row.UniqueId;

			return user;
		}
	}
}
