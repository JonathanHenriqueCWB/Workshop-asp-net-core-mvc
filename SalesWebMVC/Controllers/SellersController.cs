using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        #region Construtor
        private readonly SellerDAO _selerDAO;
        private readonly DepartmentsDAO _departmentsDAO;
        public SellersController(SellerDAO sellerDAO, DepartmentsDAO departmentsDAO)
        {
            _selerDAO = sellerDAO;
            _departmentsDAO = departmentsDAO;
        }
        #endregion
        #region Index
        public IActionResult Index()
        {
            return View(_selerDAO.FindAll());
        }
        #endregion
        #region Create
        public IActionResult Create()
        {
            //Manda lista para poder selecionar uma departamento no cadastro
            ViewBag.Departments = new SelectList(_departmentsDAO.FindAll(), "Id", "Nome");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] //Previnir ataques CSRF
        public IActionResult Create(Seller seller, int drpDepartments)
        {   
            //Lista para possívelmente alterar um seller (vendedor)
            ViewBag.Departments = new SelectList(_departmentsDAO.FindAll(), "Id", "Nome");
            //Popula o objeto com o departamento
            seller.Departments = _departmentsDAO.FindToId(drpDepartments);
            //Chamada DAO
            _selerDAO.Insert(seller);

            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                //Por id ser opcional deve incluir o value junto
                var obj = _selerDAO.FindById(id.Value);
                if (obj == null)
                {
                    return NotFound();
                }
                return View(obj);
            }
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _selerDAO.Remove(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}