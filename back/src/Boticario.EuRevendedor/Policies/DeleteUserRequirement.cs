using Microsoft.AspNetCore.Authorization;

namespace Boticario.EuRevendedor.Api.Policies
{
    public class DeleteUserRequirement : IAuthorizationRequirement
    {
        public string RequiredPermission { get; }

        public DeleteUserRequirement(string requiredPermission)
        {
            RequiredPermission = requiredPermission;
        }
    }
}
