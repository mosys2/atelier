using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Common.Dto
{
    public class RequestLoginDto
    {
        public int BranchCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
    public class ResultLoginDto
    {
        public string JwtToken { get; set; }
        public string RefreshJwtToken { get; set; }

    }
}
