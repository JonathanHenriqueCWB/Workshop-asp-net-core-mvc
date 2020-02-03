using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.DAL;
using SalesWebMVC.Models;

namespace SalesWebMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        #region Construtor
        private readonly DepartmentsDAO _departmentsDAO;

        public DepartmentsController(DepartmentsDAO departmentsDAO)
        {
            _departmentsDAO = departmentsDAO;
        }
        #endregion
        #region Index
        public async Task<IActionResult> Index()
        {
            return View(await _departmentsDAO.FindAllAsync());
        }
        #endregion
        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Departments departments)
        {
            if (ModelState.IsValid)
            {
                await _departmentsDAO.CreateAsync(departments);
                return RedirectToAction(nameof(Index));
            }
            return View(departments);
        }
        #endregion
        #region Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var departments = await _departmentsDAO.FindToIdAsync(id.Value);
            if (departments == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(departments);
        }
        #endregion
        #region Update
        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var departments = await _departmentsDAO.FindToIdAsync(id);
            if (departments == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(departments);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( Departments departments)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _departmentsDAO.UpateAsync(departments);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }
            return View();
        }
        #endregion
        #region Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var obj = await _departmentsDAO.FindToIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _departmentsDAO.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
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
