using DRIYA.Platform.Models;

namespace DRIYA.Platform.Services;

public interface IBillingService
{
    Task<Invoice> CreateInvoiceAsync(Invoice invoice);
    Task<Invoice?> GetInvoiceByIdAsync(Guid id);
    Task<Invoice?> GetInvoiceByNumberAsync(string invoiceNumber);
    Task<IEnumerable<Invoice>> GetTenantInvoicesAsync(Guid tenantId);
    Task<Invoice> UpdateInvoiceStatusAsync(Guid id, string status);
    Task<bool> ProcessPaymentAsync(Guid invoiceId, string paymentMethod, decimal amount);
}
