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
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Content("Ошибки привязки: " + string.Join("; ", errors));
            }

            var pdf = _pdfService.GenerateAct(Act);
            return File(pdf, "application/pdf", $"Act_{Act.Date}_{Act.Number}.pdf");
        }

    }
}
