namespace InvocePDF.Models
{
    public class Act
    {   
        public string Number { get; set; }
        public DateTime Date { get; set; }

        public string ContractorName { get; set; }
        public string ContractorInn { get; set; }
        public string ContractorAddress { get; set; }

        public string ClientName { get; set; }
        public string ClientInn { get; set; }
        public string ClientAddress { get; set; }

        public string ContractBasis { get; set; }

        public List<ActItem> Items { get; set; } = new();
    }
}
