using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBestApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IStringLocalizer<SharedResource> localizer;

        public IndexModel(ILogger<IndexModel> logger, IStringLocalizer<SharedResource> localizer)
        {
            _logger = logger;
            this.localizer = localizer;
        }

        public void OnGet()
        {
            ViewData["Title"] = localizer["Welcome"];
        }
    }
}
