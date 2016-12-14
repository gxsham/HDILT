using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HDILT.Models
{
	public class Post
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public DateTime Time { get; set; }
		public File LeftFile { get; set; }
		public File RightFile { get; set; }
		public int PersonId { get; set; }
		public virtual Person Person { get; set; }
		public virtual ICollection<Comment> Comments { get; set; }
		public virtual ICollection<Vote> Votes { get; set; }
		
	}
}