using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HDILT.Models;

namespace HDILT.Controllers
{
	[Authorize]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index()
        {
			var posts = db.Posts.Include(s => s.LeftFile).Include(s => s.RightFile).ToList();
            return View(posts);
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			Post post = db.Posts.Include(s => s.LeftFile).Include(s => s.RightFile).SingleOrDefault(s => s.Id == id);
			if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.PersonId = new SelectList(db.Persons, "Id", "Name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Text")] Post post, HttpPostedFileBase upload1, HttpPostedFileBase upload2)
        {
			
            if (ModelState.IsValid)
            {
				var person = db.Persons.SingleOrDefault(x => x.UserMail == User.Identity.Name);
				if (upload1 != null  && upload1.ContentLength > 0 )
				{
					
					var leftFile = new File
					{
						Name = System.IO.Path.GetFileName(upload1.FileName),
						FileType = FileType.Photo,
						ContentType = upload1.ContentType,
						Person = person
					};
					
					using (var reader = new System.IO.BinaryReader(upload1.InputStream))
					{
						leftFile.Content = reader.ReadBytes(upload1.ContentLength);
					}
					post.LeftFile = leftFile;
				}

				if ( upload2 != null && upload2.ContentLength > 0)
				{
					
					var rightFile = new File
					{
						Name = System.IO.Path.GetFileName(upload2.FileName),
						FileType = FileType.Photo,
						ContentType = upload2.ContentType,
						Person = person
						
					};
					using (var reader = new System.IO.BinaryReader(upload2.InputStream))
					{
						rightFile.Content = reader.ReadBytes(upload2.ContentLength);
					}
					
					post.RightFile = rightFile;
					
				}
				post.Person = person;
				post.Time = DateTime.Now;
				db.Posts.Add(post);
				db.SaveChanges();
                return RedirectToAction("Index");
            }
			
            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
           
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
          
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

		//Creates a vote for post
		
		public void Vote( int value, int id)
		{
			Post post = db.Posts.Find(id);
			Person person = db.Persons.FirstOrDefault(x => x.UserMail == User.Identity.Name);
			var vote = new Vote {VoteType = (VoteType)value,
								 Post = post,
								 Voter = person };
			if (db.Votes.Any(x => x.PostId == id && x.Voter.Id == person.Id))
			{
				var editVote = db.Votes.FirstOrDefault(x => x.PostId == id && x.Voter.Id == person.Id);
				if(editVote.VoteType == (VoteType)value)
				{
					db.Votes.Remove(editVote);
				}
				else
				{
					editVote.VoteType = (VoteType)value;
				}
			}
			else
			{
				db.Votes.Add(vote);
			}
			
			db.SaveChanges();
		}

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
