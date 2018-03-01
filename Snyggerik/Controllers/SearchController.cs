using Snyggerik.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snyggerik.Controllers
{
    public class SearchController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        


        [HttpGet]
        public JsonResult GetData(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return null;
            }

            var decodedKeyword = HttpUtility.UrlDecode(keyword);
            
            var blogs = db.Blogs.ToList();

            List<Blog> blogList = new List<Blog>();

            var lowerWord = keyword.ToLower();

            foreach (var blog in blogs)
            {

                if (blog.BlogTitle.ToLower().Contains(lowerWord) ||
                    blog.BlogBody.ToLower().Contains(lowerWord))

                {
                    var newBlog = new Blog();

                    newBlog.BlogId = blog.BlogId;
                    newBlog.BlogBody = blog.BlogBody;
                    newBlog.BlogTitle = blog.BlogTitle;
                    newBlog.BlogCreated = blog.BlogCreated;

                    blogList.Add(newBlog);

                }

            }
            
            return Json(blogList, JsonRequestBehavior.AllowGet);

        }
        



        [HttpGet]
        public JsonResult GetTagData(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return null;
            }

            var decodedKeyword = HttpUtility.UrlDecode(keyword);
            
            var tags = db.Tags.ToList();

            List<Tag> tagList = new List<Tag>();
            var lowerWord = keyword.ToLower();
            
            foreach (var tag in tags)
            {
                if (tag.Name.ToLower().Contains(lowerWord))

                {

                    var newTag = new Tag();                    
                    newTag.TagId = tag.TagId;
                    newTag.Name = tag.Name; 

                    tagList.Add(newTag);

                }
            }
       return Json(tagList, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetPostData(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return null;
            }

            var decodedKeyword = HttpUtility.UrlDecode(keyword);

            var posts = db.Posts.ToList();

            List<Post> postList = new List<Post>();
            var lowerWord = decodedKeyword.ToLower();


            foreach (var post in posts)
            {
                if (post.PostTitle.ToLower().Contains(lowerWord) || 
                    post.PostBody.ToLower().Contains(lowerWord))

                {

                    var newPost = new Post();

                    newPost.PostBody    =   post.PostBody;
                    newPost.PostTitle   =   post.PostTitle;                    
                    newPost.IdPost      =   post.IdPost;
                    newPost.PostCreated =   post.PostCreated;


                    postList.Add(newPost);

                }
            }
            return Json(postList, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Search(int? id)
        {

            

            return View("Search");
        }


    }
}
