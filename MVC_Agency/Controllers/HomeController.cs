using Business.Service.Abstracts;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace MVC_Agency.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IPortfolioService _portfolioService;

        public HomeController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        public IActionResult Index()
        {
            var result =_portfolioService.GetAllPortfolio();
            return View(result);
        }

       
    }
}