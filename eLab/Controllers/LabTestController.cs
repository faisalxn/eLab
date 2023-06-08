using eLab.Data;
using eLab.Repository.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eLab.Controllers
{
    public class LabTestController : Controller
    {
        private readonly ILogger<LabTestController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public LabTestController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, ILogger<LabTestController> logger)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: LabTestsController
        public ActionResult Index()
        {
            var result = _unitOfWork.LabTestRepository.GetAllTest().ToList();
            return View(result);
        }

        // GET: LabTestsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LabTestsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LabTestsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LabTest labTest)
        {
            try
            {
                var userName = HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);

                labTest.CreatedAt = DateTime.Now;
                labTest.CreatedBy = user.Id;
                labTest.IsActive = true;
                labTest.IsDeleted = false;

                await _unitOfWork.LabTestRepository.Add(labTest);
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LabTestsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LabTestsController/Edit/5
        [HttpPost]
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

        // GET: LabTestsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LabTestsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
