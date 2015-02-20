using RSR.Data.Attributes;

namespace RSR.Data.Models
{
	public class Subscription
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string Name { get; set; }
		public string XmlUrl { get; set; }
		public string HtmlUrl { get; set; }

		[Ignore]
		public int UnreadEntries { get; set; }
	}
}
