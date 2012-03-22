using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ScoutsWebsite.Models;

namespace ScoutsWebsite.IRepositories
{
    public interface IPostRepository
    {
        List<PostDetailItem> GetLatestNewsItems();

        List<PostDetailItem> GetPosts();
        void AddOrUpdatePost(PostDetailItem model);
        PostDetailItem GetPost(Guid id);
        void DeletePost(Guid id);

    }
}