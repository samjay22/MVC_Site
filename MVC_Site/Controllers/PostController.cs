using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Site.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService) {
            _postService = postService;
        }

        // GET: PostController
        [HttpGet("/Posts")]
        public ActionResult Index()
        {
            return Ok(_postService.GetLatestPosts());
        }

        // GET: PostController/Details/5
        [HttpGet("/Details/{id}")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostController/Create
        [HttpGet("/Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostController/Create
        [HttpPost("/Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Edit/5
        [HttpGet("/Update/{id}")]
        public ActionResult Update(int id)
        {
            return View();
        }

        // POST: PostController/Edit/5
        [HttpPost("/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        [HttpGet("/Delete/{id}")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostController/Delete/5
        [ValidateAntiForgeryToken]
        [HttpPost("/Delete/{id}")]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
