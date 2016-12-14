using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HDILT.Models
{
	public enum VoteType
	{
		Left, Right
	}
	public class Vote
	{
		public int Id { get; set; }

		public VoteType VoteType { get; set; }
		public Person Voter { get; set; }
		public int PostId { get; set;}
		public virtual Post Post { get; set; }
	}
}