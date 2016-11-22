using System.Web.Mvc;
using xPlay.ViewModels;

namespace xPlay.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var vm = new ChannelsViewModel().Load();
            return View(vm);
        }

    }

    public class ChannelController : Controller
    {
        public ActionResult Index(int id)
        {
            var vm = new ProgramsViewModel(id);
            return View(vm);
        }
    }
}