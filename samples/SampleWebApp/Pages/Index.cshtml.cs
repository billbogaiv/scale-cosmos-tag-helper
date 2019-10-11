using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SampleWebApp.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(ILogger<IndexModel> logger)
        {
            this.logger = logger;
        }

        private readonly ILogger<IndexModel> logger;

        public void OnGet()
        {
        }
    }
}
