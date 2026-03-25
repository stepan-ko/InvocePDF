using InvocePDF.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace InvocePDF.Services.Pdf
{
    public class InvoiceDocument : IDocument
    {
        private readonly Invoice _invoice;

        public InvoiceDocument(Invoice invoice)
        {
            _invoice = invoice;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public void Compose(IDocumentContainer container)
        {
            StaticData.counterNumber++;
            container.Page(page =>
            {
                page.Margin(40);
                page.Size(PageSizes.A4);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .Text("СЧЕТ-ФАКТУРА / АКТ ВЫПОЛНЕННЫХ РАБОТ")
                    .SemiBold().FontSize(16).AlignCenter();

                page.Content().Column(col =>
                {
                    // Исполнитель и заказчик
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Column(c =>
                        {
                            c.Item().Text($"Исполнитель: {_invoice.SellerName}");
                            c.Item().Text($"ИНН: {_invoice.SellerInn}");
                        });
                        row.RelativeItem().Column(c =>
                        {
                            c.Item().Text($"Заказчик: {_invoice.BuyerName}");
                        });
                    });

                    col.Item().LineHorizontal(1);
                    

                    // Таблица услуг
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(6); // Описание
                            columns.RelativeColumn(2); // Сумма
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Услуга/Работа");
                            header.Cell().Element(CellStyle).Text("Сумма, ₽");
                        });

                        // Строка услуги
                        table.Cell().Element(CellStyle).Text(_invoice.Description);
                        table.Cell().Element(CellStyle).Text($"{_invoice.Amount:F2}");
                    });

                    col.Item().LineHorizontal(1);

                    // Итого
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Text("");
                        row.RelativeItem().Text($"Итого: {_invoice.Amount:F2} ₽").Bold();
                    });

                    // Дата и подписи
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Column(c =>
                        {
                            c.Item().Text("Подпись исполнителя: _____________");
                        });
                        row.RelativeItem().Column(c =>
                        {
                            c.Item().Text("Подпись заказчика: _____________");
                        });
                    });

                    col.Item().Text($"Дата: {DateTime.Now:dd.MM.yyyy}").AlignRight();
                });
            });
        }

        private IContainer CellStyle(IContainer container)
        {
            return container
                .Border(1)
                .BorderColor(Colors.Black)
                .Padding(5);
        }


       
    }
}
