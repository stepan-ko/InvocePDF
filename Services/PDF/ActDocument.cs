using System.Globalization;
using InvocePDF.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

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
        var culture = new CultureInfo("ru-RU");

        container.Page(page =>
        {
            page.Margin(40);

            page.Content().Column(col =>
            {
                col.Spacing(10);

                // Заголовок
                col.Item().AlignCenter().Text("Акт приема-передачи оказанных услуг")
                    .FontSize(16).Bold();

                col.Item().AlignCenter().Text(
                    $"№ {_act.Number} от {_act.Date:dd MMMM yyyy} г."
                );

                // Исполнитель
                col.Item().Text("Исполнитель:").Bold();
                col.Item().Text(
                    $"{_act.ContractorName}, ИНН {_act.ContractorInn}, {_act.ContractorAddress}"
                );

                // Заказчик
                col.Item().Text("Заказчик:").Bold();
                col.Item().Text(
                    $"{_act.ClientName}, ИНН {_act.ClientInn}, {_act.ClientAddress}"
                );

                // Основание
                col.Item().Text($"Основание: {_act.ContractBasis}");

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
                        header.Cell().Element(CellStyle).Text("№").Bold();
                        header.Cell().Element(CellStyle).Text("Наименование").Bold();
                        header.Cell().Element(CellStyle).Text("Кол-во").Bold();
                        header.Cell().Element(CellStyle).Text("Ед.").Bold();
                        header.Cell().Element(CellStyle).Text("Цена").Bold();
                        header.Cell().Element(CellStyle).Text("Сумма").Bold();
                    });

                    // Rows
                    foreach (var item in _act.Items)
                    {
                        table.Cell().Element(CellStyle).Text(item.Number.ToString());
                        table.Cell().Element(CellStyle).Text(item.Name);
                        table.Cell().Element(CellStyle).Text(item.Quantity.ToString("0.##", culture));
                        table.Cell().Element(CellStyle).Text(item.Unit);
                        table.Cell().Element(CellStyle).Text(item.Price.ToString("N2", culture));
                        table.Cell().Element(CellStyle).Text(item.Total.ToString("N2", culture));
                    }
                });

                var total = _act.Items.Sum(x => x.Total);

                // Итоги
                col.Item().AlignRight().Text($"Итого: {total:N2}").Bold();

                col.Item().Text("Без налога (НДС) -");

                col.Item().Text(
                    $"Всего оказано услуг {_act.Items.Count}, на сумму {total:N2} руб."
                );

                col.Item().Text(NumberToWords(total));

                // Текст
                col.Item().Text(
                    "Вышеперечисленные услуги выполнены полностью и в срок. Заказчик претензий не имеет."
                );

                // Подписи
                col.Item().PaddingTop(30).Row(row =>
                {
                    row.RelativeItem().Column(c =>
                    {
                        c.Item().Text("ИСПОЛНИТЕЛЬ").Bold();
                        c.Item().Text(_act.ContractorName);
                    });

                    row.RelativeItem().Column(c =>
                    {
                        c.Item().Text("ЗАКАЗЧИК").Bold();
                        c.Item().Text(_act.ClientName);
                    });
                });
            });
        });
    }

    private IContainer CellStyle(IContainer container)
    {
        return container.Border(1).Padding(5);
    }

    // Упрощённое преобразование числа в текст
    private string NumberToWords(decimal amount)
    {
        return $"{amount:N2} рублей"; // можно улучшить позже
    }
}