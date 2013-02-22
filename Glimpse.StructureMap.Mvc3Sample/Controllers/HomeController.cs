//
using System.Web.Mvc;

namespace Glimpse.StructureMap.Mvc3Sample.Controllers
{
    public class HomeController : Controller
    {
        private IFooBar _bar;
        private IFooBar2 _bar2;
        private IFooBar3 _bar3;

        public HomeController(IFooBar bar, IFooBar2 bar2, IFooBar3 bar3)
        {
            _bar = bar;
            _bar2 = bar2;
            _bar3 = bar3;
        }
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            _bar.Name = "Snickers";
            ViewBag.Bar = _bar;
            _bar2.Name = "KitKat";
            ViewBag.Bar2 = _bar2;
            _bar3.Name = "Rollo";
            ViewBag.Bar3 = _bar3;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
