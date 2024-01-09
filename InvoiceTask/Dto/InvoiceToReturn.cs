using InvoiceTask.Models;

namespace InvoiceTask.Dto
{
    public class InvoiceToReturn
    {
        public long Id { get; set; }
        public string CustomerName { get; set; } = null!;
        public DateTime Invoicedate { get; set; }
        public ICollection<InvoiceItemToReturn> InvoiceDetails { get; set; }=new List<InvoiceItemToReturn>();
    }
}
