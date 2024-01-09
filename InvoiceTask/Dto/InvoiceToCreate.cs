namespace InvoiceTask.Dto
{
    public class InvoiceToCreate
    {
        public InvoiceToCreate()
        {
            Invoicedate = DateTime.Now;
        }
        public string CustomerName { get; set; } = null!;
        public DateTime Invoicedate { get; set; }
        public int? CashierId { get; set; }
        public int BranchId { get; set; }
    }
}
