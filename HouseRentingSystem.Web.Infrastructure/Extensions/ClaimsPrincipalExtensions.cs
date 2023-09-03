namespace HouseRentingSystem.Web.Infrastructure.Extensions
{
	using System.Security.Claims;
	using static Common.GeneralApplicationConstants;
	public static class ClaimsPrincipalExtensions
	{
		public static string? GetId(this ClaimsPrincipal user)
		=> user.FindFirstValue(ClaimTypes.NameIdentifier);

		public static bool IsAdmin(this ClaimsPrincipal user)
		=> user.IsInRole(AdminRoleName);
	}
}
