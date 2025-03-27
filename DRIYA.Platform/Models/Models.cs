namespace DRIYA.PlatformAPI.Models
{


    public class Invoice
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        // Navigation property
        public User User { get; set; }
    }

    public class Payment
    {
        public int PaymentId { get; set; }
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }

        public Invoice Invoice { get; set; }
    }

    public class Subscription
    {
        public int SubscriptionId { get; set; }
        public string PlanName { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
