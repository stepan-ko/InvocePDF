using InvocePDF.Models;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace InvocePDF.Services.PDF
{
    public class TestDocument : IDocument
    {
        //private readonly Invoice _invoice;

        //public TestDocument()
        //{
        //    _invoice = invoice;
        //}

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(40);

                page.Content().Column(col =>
                {
                    col.Spacing(10);

                    // Заголовок
                    col.Item().AlignCenter().Text("Акт приема-передачи оказанных услуг")
                        .FontSize(16).Bold();

                    col.Item().AlignCenter().Text("№ 10 от 19 сентября 2025 г.")
                        .FontSize(12);

                    // Исполнитель
                    col.Item().Text("Исполнитель:").Bold();
                    col.Item().Text(
                        "ООО \"ЗЕМНОЕ ВРЕМЯ\", ИНН 5031094224, 143005, Московская область, г. Одинцово..."
                    );

                    // Заказчик
                    col.Item().Text("Заказчик:").Bold();
                    col.Item().Text(
                        "ООО \"УСПЕХ\", ИНН 7728764275, 125445, г. Москва..."
                    );

                    // Основание
                    col.Item().Text("Основание: Договор № 8-25 от 15.08.2025");

                    // Таблица
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(30);
                            columns.RelativeColumn(4);
                            columns.ConstantColumn(50);
                            columns.ConstantColumn(40);
                            columns.ConstantColumn(70);
                            columns.ConstantColumn(80);
                        });

                        // Header
                        table.Header(header =>
                        {
                            header.Cell().Text("№").Bold();
                            header.Cell().Text("Наименование").Bold();
                            header.Cell().Text("Кол-во").Bold();
                            header.Cell().Text("Ед.").Bold();
                            header.Cell().Text("Цена").Bold();
                            header.Cell().Text("Сумма").Bold();
                        });

                        // Row 1
                        table.Cell().Text("1");
                        table.Cell().Text("Письменный перевод...");
                        table.Cell().Text("17");
                        table.Cell().Text("лист");
                        table.Cell().Text("800,00");
                        table.Cell().Text("13 600,00");

                        // Row 2
                        table.Cell().Text("2");
                        table.Cell().Text("Устный последовательный перевод...");
                        table.Cell().Text("6");
                        table.Cell().Text("час");
                        table.Cell().Text("6 000,00");
                        table.Cell().Text("36 000,00");
                    });

                    // Итоги
                    col.Item().AlignRight().Text("Итого: 49 600,00").Bold();

                    col.Item().Text("Без налога (НДС) -");

                    col.Item().Text("Всего оказано услуг 2, на сумму 49 600,00 руб.");

                    col.Item().Text("Сорок девять тысяч шестьсот рублей 00 копеек");

                    // Текст
                    col.Item().Text(
                        "Вышеперечисленные услуги выполнены полностью и в срок. " +
                        "Заказчик претензий не имеет."
                    );

                    // Подписи
                    col.Item().PaddingTop(30).Row(row =>
                    {
                        row.RelativeItem().Column(c =>
                        {
                            c.Item().Text("ИСПОЛНИТЕЛЬ").Bold();
                            c.Item().Text("ООО \"ЗЕМНОЕ ВРЕМЯ\"");
                        });

                        row.RelativeItem().Column(c =>
                        {
                            c.Item().Text("ЗАКАЗЧИК").Bold();
                            c.Item().Text("ООО \"УСПЕХ\"");
                        });
                    });
                });
            });
        }


    }
}
