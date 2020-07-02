using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Api.Policies
{
    public class DeleteUserRequirementHandler : AuthorizationHandler<DeleteUserRequirement>
    {
        private const string AdministratorRoleName = "Administrador";

        private AuthorizationHandlerContext _context;

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DeleteUserRequirement requirement)
        {
            _context = context;

            var isAdministrator = IsAdministrator();
            var canDeleteUser = HasRequirements(requirement);

            if (isAdministrator && canDeleteUser)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        private bool IsAdministrator()
        {
            return GetClaim(ClaimTypes.Role, AdministratorRoleName);
        }

        private bool HasRequirements(DeleteUserRequirement requirement)
        {
            return GetClaim("permissions", requirement.RequiredPermission);
        }

        private bool GetClaim(string type, string value)
        {
            return _context.User.HasClaim(type, value);
        }
    }
}
