//
using System.Web.Mvc;

namespace Glimpse.StructureMap.Mvc3Sample.Controllers
{
    public class HomeController : Controller
    {
        private IFooBar _bar;

        public HomeController(IFooBar bar)
        {
            _bar = bar;
        }
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            _bar.Name = "Snickers";
            ViewBag.Bar = _bar;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
