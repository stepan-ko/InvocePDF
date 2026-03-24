using InvocePDF.Models;
using InvocePDF.Services.Pdf;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace InvocePDF.Services
{
    public class PdfService
    {
        public byte[] GenerateInvoice(Invoice invoice)
        {
            var document = new InvoiceDocument(invoice);
            return document.GeneratePdf();
        }

        public byte[] GenerateAct(Act act)
        {
            var document = new ActDocument(act);
            return document.GeneratePdf();
        }
    }
}
