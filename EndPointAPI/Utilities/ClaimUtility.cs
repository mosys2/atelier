using MongoDB.Driver.Linq;
using NuGet.Versioning;
using System.Security.Claims;

namespace EndPointAPI.Utilities
{
    public static class ClaimUtility
    {
        public static string? GetUserId(ClaimsPrincipal user)
        {
            try
            {
                if (user.Identity is ClaimsIdentity claimsIdentity &&
                    claimsIdentity.TryGetClaimValue(ClaimTypes.NameIdentifier, out string userId))
                {
                    return userId;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string? GetBranchId(ClaimsPrincipal user)
        {
            try
            {
                if (user.Identity is ClaimsIdentity claimsIdentity &&claimsIdentity.TryGetClaimValue("BranchId", out string branchId))
                {
                    return branchId;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static bool TryGetClaimValue(this ClaimsIdentity claimsIdentity, string claimType, out string value)
        {
            var claim = claimsIdentity.FindFirst(claimType);
            if (claim != null)
            {
                value = claim.Value;
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }
    }

}
