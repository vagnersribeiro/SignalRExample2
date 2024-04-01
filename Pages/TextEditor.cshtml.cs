using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SignalRExample2.Pages
{
    public class TextEditorModel : PageModel
    {
        private readonly ILogger<TextEditorModel> _logger;

        public TextEditorModel(ILogger<TextEditorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }

}
