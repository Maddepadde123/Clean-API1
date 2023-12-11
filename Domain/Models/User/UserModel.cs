using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.User
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}
