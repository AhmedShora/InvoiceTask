namespace InvoiceTask.Dto
{
    public class InvoiceItemToEdit
    {
        public string ItemName { get; set; } = null!;
        public double ItemCount { get; set; }
        public double ItemPrice { get; set; }
    }
}
