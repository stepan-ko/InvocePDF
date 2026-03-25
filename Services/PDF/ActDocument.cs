using System.Data.Common;
using System.Globalization;
using System.Reflection.Emit;
using InvocePDF.Models;
using InvocePDF.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using static QuestPDF.Helpers.Colors;

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

        StaticData.counterNumber++;

        container.Page(page =>
        {
            page.Margin(40);

            page.Content().Column(col =>
            {
                col.Spacing(10);

                // Заголовок
                col.Item().AlignCenter().Text($"Акт приема-передачи оказанных услуг № {StaticData.counterNumber} от {_act.Number} г.")
                    .FontSize(16).Bold();

                // Номер дата
                //col.Item().AlignCenter().Text(
                //    $"№ {StaticData.counterNumber} от {_act.Number} г."
                //).FontSize(16).Bold();

                // Линия
                col.Item()
                    .PaddingVertical(1)
                    .LineHorizontal(1);

                // Исполнитель
                col.Item().Row(row =>
                 {
                     row.ConstantItem(100) 
                         .AlignMiddle()
                         .AlignLeft()                         
                         .Text("Исполнитель:").FontSize(10);

                     row.RelativeItem()
                         .AlignMiddle()
                         .AlignLeft()
                         .Text($"{_act.ContractorName}, ИНН {_act.ContractorInn}, {_act.ContractorAddress}").Bold().FontSize(11);
                 });

                // Заказчик
                col.Item().Row(row =>
                {
                    row.ConstantItem(100)
                            .AlignMiddle()
                            .AlignLeft()
                            .Text("Заказчик:").FontSize(10);

                    row.RelativeItem()
                            .AlignMiddle()
                            .AlignLeft()
                            .Text($"{_act.ClientName}, ИНН {_act.ClientInn}, {_act.ClientAddress}").Bold().FontSize(11);
                });

                // Основание
                col.Item().Row(row =>
                {
                    row.ConstantItem(100)
                            .AlignMiddle()
                            .AlignLeft()
                            .Text("Основание:").FontSize(10);

                    row.RelativeItem()
                            .AlignMiddle()
                            .AlignLeft()
                            .Text(_act.ContractBasis).Bold().FontSize(11);
                });

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
                col.Item().AlignRight().Text($"Итого:  {total:N2}").Bold();
                                

                col.Item().Text(
                    $"Всего оказано услуг {_act.Items.Count}, на сумму {total:N2} руб."
                );

                col.Item().Text(MoneyToText.Convert(total)).Bold().FontSize(12);

                // Текст
                col.Item().Text(
                    "Вышеперечисленные услуги выполнены полностью и в срок. Заказчик претензий по объему, качеству и срокам оказания услуг не имеет."
                );

                // Линия
                col.Item()
                    .PaddingVertical(10)
                    .LineHorizontal(1);

                // Подписи
                col.Item().PaddingTop(10).Row(row =>
                {
                    row.RelativeItem().PaddingRight(5).Column(c =>
                    {
                        c.Item().Text("ИСПОЛНИТЕЛЬ").Bold();
                        c.Item().PaddingTop(10).Text(_act.ContractorName);
                        // Линия
                        c.Item()
                            .PaddingTop(20)
                            .LineHorizontal(1);
                    });

                    row.RelativeItem().PaddingLeft(5).Column(c =>
                    {
                        c.Item().Text("ЗАКАЗЧИК").Bold();
                        c.Item().PaddingTop(10).Text(_act.ClientName);
                        // Линия
                        c.Item()
                            .PaddingTop(20)
                            .LineHorizontal(1);
                    });
                });

                

            });
        });
    }

    private IContainer CellStyle(IContainer container)
    {
        return container.Border(1).Padding(5);
    }

    
}