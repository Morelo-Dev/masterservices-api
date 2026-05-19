namespace MasterServicesAPI.Security
{
    using Microsoft.AspNetCore.Authorization;

    public class RoleRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }

        public RoleRequirement(string permission)
        {
            Permission = permission;
        }
    }


}
