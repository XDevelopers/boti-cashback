using System;

namespace Boticario.EuRevendedor.Core.Security
{
    public class RefreshToken
    {
        public string Id { get; set; }

        public string Email { get; set; }
        
        public string Token { get; set; }
        
        public DateTime Expiration { get; set; }
    }
}
