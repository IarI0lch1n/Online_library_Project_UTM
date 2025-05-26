using System;
using System.Web.Mvc;
using Online_lib.BusinessLogic;
using Online_lib.BusinessLogic.DBModel;
using Online_lib.Domain.Entities.User;

namespace Online_lib.Web.Controllers
{
    public class AdminPublisherManagementController : Controller
    {
        private readonly PublisherBL _publisherBL;

        public AdminPublisherManagementController()
        {
            _publisherBL = new PublisherBL(new UserContext());
        }

        public ActionResult Index()
        {
            var publishers = _publisherBL.GetAllPublishers();
            return View("~/Views/Shared/_AdminPublisherManagement.cshtml", publishers);
        }

        [HttpPost]
        public ActionResult AddPublisher(string name, string country, DateTime registrationDate)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var publisher = new Publisher
                {
                    Name = name,
                    Country = country,
                    RegistrationDate = registrationDate
                };
                _publisherBL.AddPublisher(publisher);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _publisherBL.DeletePublisher(id);
            return RedirectToAction("Index");
        }
    }
}
