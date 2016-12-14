using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HDILT.Models
{
	public enum FileType
	{
		Photo, Audio, Video
	}
	public class File
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public byte[] Content { get; set; }
		public FileType FileType { get; set; }
		public string ContentType { get; set; }
		public int PersonId { get; set; }
		public virtual Person Person { get; set; }
	}
}