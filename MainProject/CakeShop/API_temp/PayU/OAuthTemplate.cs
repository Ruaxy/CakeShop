using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TAI_RLaba_PNakielny.API.PayU
{
    public class OAuthTemplate
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string grant_type { get; set; }
    }
}
