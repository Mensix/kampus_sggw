using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampusSggwBackend.Domain.JwtToken
{
    public class RefreshToken
    {
        public Guid UserId { get; set; }
        public string TokenId { get; set; }
        public string TokenRole { get; set; }
        public DateTimeOffset Expiration { get; set; }
    }
}
