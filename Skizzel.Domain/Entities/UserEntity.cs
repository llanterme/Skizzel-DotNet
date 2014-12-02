using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skizzel.Domain.Entities
{
    public class UserEntity
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public List<ReceiptEntity> ReceiptList { get; set; }
        public List<MillageEntity> MillageList { get; set; } 
    }
}
