using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using SalesWebMVC.DAL.Exceptions;
using System.Diagnostics;
using System;

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
        #region Details
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = _selerDAO.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }
        #endregion
        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            //Por id ser opcional deve incluir o value junto
            var obj = _selerDAO.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View();
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _selerDAO.Remove(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Edit
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = _selerDAO.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            ViewBag.Departments = new SelectList(_departmentsDAO.FindAll(), "Id", "Nome");
            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Seller obj, int drpDepartments)
        {
            try
            {
                obj.Departments = _departmentsDAO.FindToId(drpDepartments);
                _selerDAO.Update(obj);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }
        #endregion
        #region Error
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
        #endregion
    }
}