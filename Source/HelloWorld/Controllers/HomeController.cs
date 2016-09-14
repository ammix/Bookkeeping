using System.Web.Mvc;

namespace HelloWorld.Controllers
{
    public class HomeController : Controller
    {
		// GET: Home
		//public ActionResult Index()
		public string Index()
		{
			//return View();
			return "Hello world!";
        }
    }
}