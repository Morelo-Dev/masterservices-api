using Microsoft.AspNetCore.Authorization;

namespace MasterServicesAPI.Security
{
    public class RoleAuthorizationHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            // 🔹 Obtener el RoleId del usuario desde el JWT
            var roleClaim = context.User.Claims
                .FirstOrDefault(c => c.Type == "RoleId");

            if (roleClaim != null && int.TryParse(roleClaim.Value, out int userRoleId))
            {
                // 🔹 Mapeo de roles y permisos
                var permissionsByRole = new Dictionary<int, List<string>>
                {
                    { 1, new List<string> { "Leer", "Crear", "Actualizar", "Eliminar" } }, // Admin
                    { 2, new List<string> { "Leer", "Crear", "Actualizar" } }, // Gerente
                    { 3, new List<string> { "Leer" } } // Técnico
                };

                // ✅ Si el usuario tiene el permiso, conceder acceso
                if (permissionsByRole.TryGetValue(userRoleId, out var permissions) &&
                    permissions.Contains(requirement.Permission))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail(); // Denegar acceso si no tiene el permiso
                }
            }
            else
            {
                context.Fail(); // Si no tiene el claim "RoleId", denegar acceso
            }

            return Task.CompletedTask;
        }
    }
}
