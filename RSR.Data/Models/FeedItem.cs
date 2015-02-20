using System;

namespace RSR.Data.Models
{
	public class FeedItem
	{
		public string Title { get; set; }
		public string Link { get; set; }
		public string Description { get; set; }
		public string Author { get; set; }
		public DateTime PublishDate { get; set; }
		public string Guid { get; set; }
	}
}
