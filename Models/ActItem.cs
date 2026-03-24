namespace InvocePDF.Models
{
    public class ActItem
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }

        public decimal Total => Quantity * Price;
    }
}
