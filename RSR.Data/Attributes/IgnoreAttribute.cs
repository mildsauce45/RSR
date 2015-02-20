using System;
using RSR.Core.Support;

namespace RSR.Data.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple=false)]
	public class IgnoreAttribute : Attribute
	{
		public CrudType CrudType { get; private set; }

		public IgnoreAttribute(CrudType type = CrudType.Create | CrudType.Update)
		{
			CrudType = type;
		}
	}
}
