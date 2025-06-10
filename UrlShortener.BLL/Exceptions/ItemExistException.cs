using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.BLL.Exceptions
{
    public class ItemExistException: Exception
    {
        public ItemExistException() : base() { }

        public ItemExistException(string message) : base(message) { }

        public ItemExistException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
