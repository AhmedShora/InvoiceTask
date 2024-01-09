namespace InvoiceTask.Dto
{
    public class InvoiceItemToCreate
    {
        public long InvoiceHeaderId { get; set; }
        public string ItemName { get; set; } = null!;
        public double ItemCount { get; set; }
        public double ItemPrice { get; set; }
    }
}
