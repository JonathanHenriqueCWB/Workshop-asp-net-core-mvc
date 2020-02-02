using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using SalesWebMVC.DAL.Exceptions;
using System.Diagnostics;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            var list = await _selerDAO.FindAllAsync();
            return View(list);
        }
        #endregion
        #region Create
        public async Task<IActionResult> Create()
        {
            //Manda lista para poder selecionar uma departamento no cadastro
            ViewBag.Departments = new SelectList(await _departmentsDAO.FindAllAsync(), "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] //Previnir ataques CSRF
        public async Task<IActionResult> Create(Seller seller, int drpDepartments)
        {
            //Validação por parte do servidor
            if (ModelState.IsValid)
            {
                //Popula o objeto com o departamento
                seller.Departments = await _departmentsDAO.FindToIdAsync(drpDepartments);
                //Chamada DAO
                await _selerDAO.InsertAsync(seller);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        #endregion
        #region Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _selerDAO.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }
        #endregion
        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            //Por id ser opcional deve incluir o value junto
            var obj = await _selerDAO.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _selerDAO.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _selerDAO.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            ViewBag.Departments = new SelectList(await _departmentsDAO.FindAllAsync(), "Id", "Nome");
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Seller obj, int drpDepartments)
        {
            //Validação por parte do servidor
            if (ModelState.IsValid)
            {
                try
                {
                    obj.Departments = await _departmentsDAO.FindToIdAsync(drpDepartments);
                    await _selerDAO.UpdateAsync(obj);
                    return RedirectToAction(nameof(Index));
                }
                catch (ApplicationException e)
                {
                    return RedirectToAction(nameof(Error), new { message = e.Message });
                }
            }
            return View();
        }
        #endregion
        #region Error
        /*Método retorna a view de erro tipada errorViewModel 
        criada automaticamente na criação do projeto
        Será chamado pelos demais métodos acima passando como
        parametro um msg de erro personalizada*/
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