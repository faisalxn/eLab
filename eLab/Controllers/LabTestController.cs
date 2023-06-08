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
            var labTest = _unitOfWork.LabTestRepository.GetById(id);
            return View(labTest);
        }

        // GET: LabTestsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LabTestsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(LabTest labTest)
        {
            try
            {
                var userName = HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);

                labTest.CreatedAt = DateTime.Now;
                labTest.CreatedBy = user.Id;
                labTest.IsActive = true;
                labTest.IsDeleted = false;

                _unitOfWork.LabTestRepository.Add(labTest);
                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LabTestsController/Edit/5
        public ActionResult EditAsync(int id)
        {
            var labTest = _unitOfWork.LabTestRepository.GetById(id);
            return View(labTest);
        }

        // POST: LabTestsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, LabTest labTest)
        {
            try
            {
                var userName = HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);

                var test = _unitOfWork.LabTestRepository.GetById(id);

                test.Name = labTest.Name;
                test.Price = labTest.Price;
                test.Category = labTest.Category;
                test.Description = labTest.Description;
                test.Preparation = labTest.Preparation;
                test.IsActive = labTest.IsActive;

                test.UpdatedAt = DateTime.Now;
                test.UpdatedBy = user.Id;

                _unitOfWork.Save();

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
            var labTest = _unitOfWork.LabTestRepository.GetById(id);
            return View(labTest);
        }

        // POST: LabTestsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, LabTest labTest)
        {
            try
            {
                var userName = HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);

                var test = _unitOfWork.LabTestRepository.GetById(id);

                test.IsDeleted = true;

                test.UpdatedAt = DateTime.Now;
                test.UpdatedBy = user.Id;

                _unitOfWork.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
