using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HDILT.Models
{
	public class Person
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public int Age { get; set; }
		public string UserMail { get; set; }
		public int ProfilePicture { get; set; }
		public virtual ICollection<Post> Posts { get; set; }
		public virtual ICollection<File> Files { get; set; }
	}
}