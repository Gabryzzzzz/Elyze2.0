using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantService
{
    public interface ITenantService
    {
        public TenantResponse GetTenant(string company);
    }
}
