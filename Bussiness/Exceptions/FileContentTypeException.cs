using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Exceptions
{
    public  class FileContentTypeException:Exception
    {
        string PropertyName { get; set; }
        public FileContentTypeException(string propertyName,string? message) : base(message)
        {
            PropertyName = propertyName;
        }

        

    }
}
