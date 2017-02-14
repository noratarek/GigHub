using GigHub.Core;
using GigHub.Core.ViewModels;
using GigHub.Persistence;
using GigHub.Persistence.Repositories;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitofwork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _unitofwork.Gigs.GetUpcomingGigs(query);

            var userId = User.Identity.GetUserId();

            var attendances = _unitofwork.Attendances.GetFutureAttendances(userId)
                .ToLookup(a => a.GigId);

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendances = attendances
            };

            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}