using Microsoft.AspNetCore.Authentication;
using RyanP410.WebUI.Models.DataContexts;

namespace RyanP410.WebUI.AppCode.Providers
{
    public class ClaimsTransformationProvider //: IClaimsTransformation
    {
        //    readonly UserManager<AppUser> userManager;
        //    readonly RyanDbContext context;

        //    public ClaimsTransformationProvider(UserManager<AppUser> userManager, RyanDbContext context)
        //    {
        //        this.userManager = userManager;
        //        this.context = context;
        //    }

        //    async public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        //    {
        //        if (principal != null && principal.Identity.IsAuthenticated)
        //        {
        //            ClaimsIdentity identity = principal.Identity as ClaimsIdentity;

        //            var currentUser = await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserName.Equals(identity.Name));

        //            if (currentUser != null)
        //            {
        //                var userClaims = context.UserClaims.Where(x => x.UserId == currentUser.Id)
        //                    .Select(x => x.ClaimType)
        //                    .Union(from ur in context.UserRoles
        //                           join rc in context.RoleClaims on ur.RoleId equals rc.RoleId
        //                           where ur.UserId == currentUser.Id
        //                           select rc.ClaimType)
        //                    .ToArray();

        //                Claim roleClaim;

        //                foreach (var claimName in userClaims)
        //                {
        //                    roleClaim = identity.Claims.FirstOrDefault(c => c.Type == claimName);
        //                    identity.TryRemoveClaim(roleClaim);
        //                }

        //                identity.AddClaims(context.UserClaims.Where(x => x.UserId == currentUser.Id)
        //                    .Select(x => new Claim(x.ClaimType, x.ClaimValue))
        //                    .ToList()
        //                    .Union((from ur in context.UserRoles
        //                            join rc in context.RoleClaims on ur.RoleId equals rc.RoleId
        //                            where ur.UserId == currentUser.Id
        //                            select rc).Distinct().ToList().Select(x => new Claim(x.ClaimType, x.ClaimValue)))
        //                    );

        //                roleClaim = identity.Claims.FirstOrDefault(c => c.Type == identity.RoleClaimType);

        //                while (roleClaim != null)
        //                {
        //                    identity.RemoveClaim(roleClaim);
        //                    roleClaim = identity.Claims.FirstOrDefault(c => c.Type == identity.RoleClaimType);
        //                }

        //                var roles = await userManager.GetRolesAsync(currentUser);

        //                foreach (var role in roles)
        //                    identity.AddClaim(new Claim(identity.RoleClaimType, role));
        //            }
        //        }

        //        return principal;
        //    }
        //}
    }
}