namespace InvocePDF.Models
{
    public class Invoice
    {
        public string SellerName { get; set; }
        public string SellerInn { get; set; }

        public string BuyerName { get; set; }

        public string Description { get; set; }
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

    }
}
