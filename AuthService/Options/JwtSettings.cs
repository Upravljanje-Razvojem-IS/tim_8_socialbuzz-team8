using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Options
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
    }
}
