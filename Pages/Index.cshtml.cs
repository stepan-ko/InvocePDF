using System.Diagnostics;
using InvocePDF.Models;
using InvocePDF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvocePDF.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PdfService _pdfService;

        public IndexModel(PdfService pdfService)
        {
            _pdfService = pdfService;
        }

        [BindProperty]
        public Invoice Invoice { get; set; }

        public IActionResult OnPost()
        {

            //Debug.WriteLine("вызван OnPost()");
            
            if (!ModelState.IsValid)
            {
                return Content("Ошибка привязки данных формы");
            }

            //тест

            //var pdfTest = _pdfService.GenerateTest();
            //return File(pdfTest, "application/pdf", "test.pdf");
            //


            var pdf = _pdfService.GenerateInvoice(Invoice);
            return File(pdf, "application/pdf", "invoice.pdf");
        }
    }

}
