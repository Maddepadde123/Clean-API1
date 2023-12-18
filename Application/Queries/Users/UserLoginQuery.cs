using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Users
{
    public class UserLoginQuery : IRequest<string>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
