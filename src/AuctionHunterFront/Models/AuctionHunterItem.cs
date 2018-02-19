using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionHunterFront.Models
{
	public class AuctionHunterItem
	{
		public int Id { get; set; }

		[Index(IsUnique = true)]
		[StringLength(250)]
		[Required]
		public string AuctionLink { get; set; }

		[Required]
		public int OnPage { get; set; }

		[Required]
		public bool MarkedAsRead { get; set; }

		[Required]
		public DateTime Timestamp { get; set; }

		public string ContentJson { get; set; }
	}
}
