using InvocePDF.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace InvocePDF.Services.Pdf
{
    public class ActDocument : IDocument
    {
        private readonly Act _act;

        public ActDocument(Act act)
        {
            _act = act;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Content().Column(col =>
                {
                    col.Item().Text("СЧЕТ").FontSize(20).Bold();
                    col.Item().Text($"Подрядчик: {_act.ContractorName}");
                });
            });
        }
    }
}
