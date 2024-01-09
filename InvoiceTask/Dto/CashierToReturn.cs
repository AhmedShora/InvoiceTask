namespace InvoiceTask.Dto
{
    public class CashierToReturn
    {
        public int Id { get; set; }
        public string CashierName { get; set; } = null!;
        public int BranchId { get; set; }
        public string BranchName { get; set; }= null!;
    }
}
