using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScoutsWebsite.IRepositories;
using ScoutsWebsite.Models;

namespace ScoutsWebsite.Repositories
{
    public class PostRepository : IPostRepository
    {
        private ScoutsDataDataContext _db;
        public PostRepository(ScoutsDataDataContext db)
        {
            this._db = db;
        }

        public List<PostDetailItem> GetLatestNewsItems()
        {
            List<PostDetailItem> result = new List<PostDetailItem>();
            result = (from p in _db.Posts
                      orderby p.PostDate descending
                      select new PostDetailItem()
                     {
                         PostContent = p.PostContent,
                         PostId = p.PostID,
                         PostSubject = p.PostSubject,
                         PostDate = p.PostDate
                     }).Take(5).ToList();
            return result;
        }


        public List<PostDetailItem> GetPosts()
        {
            List<PostDetailItem> result = new List<PostDetailItem>();
            result = (from p in _db.Posts
                      orderby p.PostDate descending
                      select new PostDetailItem()
                      {
                          PostContent = p.PostContent,
                          PostId = p.PostID,
                          PostSubject = p.PostSubject,
                          PostDate = p.PostDate
                      }).ToList();
            return result;
        }

        public void AddOrUpdatePost(PostDetailItem model)
        {
            var post = (from x in _db.Posts where x.PostID == model.PostId select x).FirstOrDefault();
            if (post == null)
            {
                post = new Post();
                post.PostID = Guid.NewGuid();
                post.PostDate = DateTime.Now;
                _db.Posts.InsertOnSubmit(post);
            }

            post.PostContent = model.PostContent ?? "";
            post.PostSubject = model.PostSubject ?? "";
            _db.SubmitChanges();
        }

        public PostDetailItem GetPost(Guid id)
        {
            var post = (from p in _db.Posts
                        where p.PostID == id
                        select new PostDetailItem()
                        {
                            PostContent = p.PostContent,
                            PostId = p.PostID,
                            PostSubject = p.PostSubject,
                            PostDate = p.PostDate
                        }).FirstOrDefault();
            return post;
        }

        public void DeletePost(Guid id)
        {
            var post = (from x in _db.Posts
                        where x.PostID == id
                        select x).FirstOrDefault();
            if (post != null)
            {
                _db.Posts.DeleteOnSubmit(post);
                _db.SubmitChanges();
            }
        }
    }
}