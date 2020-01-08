using System;

namespace SalesWebMVC.DAL.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message) :base(message)
        {
        }
    }
}
