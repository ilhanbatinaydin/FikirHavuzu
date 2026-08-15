using FikirHavuzu.Entity.Entities;

namespace FikirHavuzu.Repository.Extensions
{
    public static class UserRepositoryExtension
    {
        public static IQueryable<User> FilteredByFullName(this IQueryable<User> users, string? fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return users;

            var lowerCaseName = fullName.Trim().ToLower();

            return users.Where(u => (u.FirstName + " " + u.LastName).ToLower().Contains(lowerCaseName));
        }

        public static IQueryable<User> FilteredByIdentityNumber(this IQueryable<User> users, string? identityNumber)
        {
            if (string.IsNullOrWhiteSpace(identityNumber))
                return users;

            return users.Where(u => u.IdentityNumber.Contains(identityNumber.Trim()));
        }

        public static IQueryable<User> FilteredByActiveStatus(this IQueryable<User> users, bool? isActive)
        {
            if (isActive is null)
                return users;

            return users.Where(u => u.IsActive == isActive.Value);
        }

        public static IQueryable<User> FilteredByPermissionId(this IQueryable<User> users, int? permissionId)
        {
            if (permissionId is null || permissionId.Value <= 0)
                return users;

            return users.Where(u => u.UserPermissions.Any(up => up.PermissionId == permissionId.Value));
        }

        public static IQueryable<User> FilteredByEmail(this IQueryable<User> users, string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return users;

            var lowerCaseTerm = email.Trim().ToLower();

            return users.Where(u => (u.Email).ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<User> ToPaginate(this IQueryable<User> users, int pageNumber, int pageSize)
        {
            return users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }
    }
}