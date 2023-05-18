using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantService
{
    public class TenantException : Exception
    {
        public TenantException()
        {
        }

        public TenantException(string message) : base(message)
        {
        }
    }
}
