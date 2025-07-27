using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Login.DTO
{
    public class AccountResponseDTO
    {
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
