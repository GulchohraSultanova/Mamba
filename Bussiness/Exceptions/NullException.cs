using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Exceptions
{
    public  class NullException:Exception
    {
        string PropertyName { get; set; }
        public NullException(string propertyname,string? message) : base(message)
        {
            PropertyName = propertyname;
        }

       

        
    }
}
