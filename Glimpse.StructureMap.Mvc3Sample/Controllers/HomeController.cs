//
using System.Web.Mvc;
//
using Glimpse.StructureMap.Sample.Core.Interfaces;

namespace Glimpse.StructureMap.Sample.Web.Controllers
{
    public class HomeController : Controller
    {
        private IFooBarService _barService;
        private IFooBar2Service _bar2Service;
        private ISomeOtherInterface _someOtherInterface;

        public HomeController(IFooBarService barService, IFooBar2Service bar2Service, ISomeOtherInterface someOtherInterface)
        {
            _barService = barService;
            _bar2Service = bar2Service;
            _someOtherInterface = someOtherInterface;
        }
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            _barService.Name = "Snickers";
            ViewBag.Bar = _barService;
            _bar2Service.Name = "KitKat";
            ViewBag.Bar2 = _bar2Service;
            _someOtherInterface.Name = "Rollo";
            ViewBag.SomeOtherInterface = _someOtherInterface;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
