using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.DAL;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerDAO _selerDAO;
        public SellersController(SellerDAO sellerDAO)
        {
            _selerDAO = sellerDAO;
        }
        public IActionResult Index()
        {
            return View(_selerDAO.FindAll());
        }
    }
}