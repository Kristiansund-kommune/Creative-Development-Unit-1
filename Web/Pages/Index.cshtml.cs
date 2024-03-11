using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
	[AllowAnonymous]
	[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
	public class IndexModel : PageModel
	{
	}
}
