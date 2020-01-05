using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.DAL
{
    public class DepartmentsDAO
    {
        #region Constructor and context
        private readonly SalesWebMVCContext _context;
        public DepartmentsDAO(SalesWebMVCContext context)
        {
            _context = context;
        }
        #endregion
        #region List
        public List<Departments> FindAll()
        {
            return _context.Departments.ToList();
        }
        #endregion
        #region Find To Id
        public Departments FindToId(int? id)
        {
            return _context.Departments.Find(id);
        }
        #endregion
    }
}
