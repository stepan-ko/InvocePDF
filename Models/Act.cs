namespace InvocePDF.Models
{
    public class Act
    {
        public string ContractorName { get; set; }
        public string ContractorInn { get; set; }

        public string ClientName { get; set; }

        public string Service { get; set; }
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
