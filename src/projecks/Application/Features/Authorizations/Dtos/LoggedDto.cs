using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Features.Authorizations.Dtos
{
    public class LoggedDto
    {
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
