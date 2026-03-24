using InvocePDF.Models;
using InvocePDF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvocePDF.Pages
{
    public class ActModel : PageModel
    {
        private readonly PdfService _pdfService;

        public ActModel(PdfService pdfService)
        {
            _pdfService = pdfService;
        }

        [BindProperty]
        public Act Act { get; set; }

        public IActionResult OnPost()
        {
            var pdf = _pdfService.GenerateAct(Act);
            return File(pdf, "application/pdf", "act.pdf");
        }
    }
}
