using System;

namespace RSR.Core.Support
{
	[Flags]
	public enum CrudType
	{
		Read = 1,
		Create = 2,
		Update = 4
	}
}
