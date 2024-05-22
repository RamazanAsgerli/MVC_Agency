using Business.CustomExceptions;
using Business.Service.Abstracts;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MVC_Agency.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="SuperAdmin")]
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        public IActionResult Index()
        {
            var portfolio = _portfolioService.GetAllPortfolio();
            return View(portfolio);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Portfolio portfolio)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                _portfolioService.AddPortfolio(portfolio);
            }
            catch (EntityNullException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch (FileNulException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch (FileLengthException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch (ContentTypeException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return RedirectToAction(nameof(Index));

        }
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                _portfolioService.DeletePortfolio(id);
            }
            catch(FileNulException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();

            }
            catch(EntityNullException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
       
        public IActionResult Update(int id)
        {
            var portofilio = _portfolioService.GetPortfolio(x => x.Id == id);
            if (portofilio == null) return View("Error");
            return View(portofilio);
        }
        [HttpPost]
        public IActionResult Update(Portfolio portfolio)
        {
            if (!ModelState.IsValid) return View();
            try
            {
                _portfolioService.UpdatePortfolio(portfolio.Id, portfolio);
            }catch(EntityNullException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch(FileLengthException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch(ContentTypeException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }
            catch(PhotoNotFoundException ex)
            {
                ModelState.AddModelError(ex.V, ex.Message);
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
