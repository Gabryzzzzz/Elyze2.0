using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantService
{
    public class TenantResponse
    {
        public string? LoginPage { get; set; }

        public string? ConnectionString { get; set; }

        public bool ExternalBi { get; set; }

        public string? PowerBiLink { get; set; }
    }
}
