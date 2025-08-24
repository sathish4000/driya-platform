using DRIYA.Platform.Data;
using DRIYA.Platform.Models;
using Microsoft.EntityFrameworkCore;

namespace DRIYA.Platform.Services;

public class BillingService : IBillingService
{
    private readonly ApplicationDbContext _context;

    public BillingService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Invoice> CreateInvoiceAsync(Invoice invoice)
    {
        invoice.CreatedAt = DateTime.UtcNow;
        invoice.InvoiceNumber = await GenerateInvoiceNumberAsync();
        
        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();
        
        return invoice;
    }

    public async Task<Invoice?> GetInvoiceByIdAsync(Guid id)
    {
        return await _context.Invoices
            .Include(i => i.Tenant)
            .Include(i => i.Plan)
            .Include(i => i.InvoiceItems)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Invoice?> GetInvoiceByNumberAsync(string invoiceNumber)
    {
        return await _context.Invoices
            .Include(i => i.Tenant)
            .Include(i => i.Plan)
            .Include(i => i.InvoiceItems)
            .FirstOrDefaultAsync(i => i.InvoiceNumber == invoiceNumber);
    }

    public async Task<IEnumerable<Invoice>> GetTenantInvoicesAsync(Guid tenantId)
    {
        return await _context.Invoices
            .Include(i => i.Plan)
            .Include(i => i.InvoiceItems)
            .Where(i => i.TenantId == tenantId)
            .OrderByDescending(i => i.CreatedAt)
            .ToListAsync();
    }

    public async Task<Invoice> UpdateInvoiceStatusAsync(Guid id, string status)
    {
        var invoice = await _context.Invoices.FindAsync(id);
        if (invoice == null)
            throw new ArgumentException($"Invoice with ID {id} not found");

        invoice.Status = status;
        invoice.UpdatedAt = DateTime.UtcNow;

        if (status == "Paid")
        {
            invoice.PaidDate = DateTime.UtcNow;
        }

        _context.Invoices.Update(invoice);
        await _context.SaveChangesAsync();
        
        return invoice;
    }

    public async Task<bool> ProcessPaymentAsync(Guid invoiceId, string paymentMethod, decimal amount)
    {
        var invoice = await _context.Invoices.FindAsync(invoiceId);
        if (invoice == null)
            return false;

        // Here you would integrate with Stripe or other payment processor
        // For now, we'll just update the invoice status
        
        invoice.Status = "Paid";
        invoice.PaidDate = DateTime.UtcNow;
        invoice.UpdatedAt = DateTime.UtcNow;
        
        // Add payment record
        var paymentItem = new InvoiceItem
        {
            InvoiceId = invoiceId,
            Description = $"Payment via {paymentMethod}",
            Quantity = 1,
            UnitPrice = amount,
            TotalPrice = amount,
            ItemType = "payment"
        };

        _context.InvoiceItems.Add(paymentItem);
        _context.Invoices.Update(invoice);
        await _context.SaveChangesAsync();
        
        return true;
    }

    private async Task<string> GenerateInvoiceNumberAsync()
    {
        var lastInvoice = await _context.Invoices
            .OrderByDescending(i => i.CreatedAt)
            .FirstOrDefaultAsync();

        if (lastInvoice == null)
            return "INV-0001";

        var lastNumber = int.Parse(lastInvoice.InvoiceNumber.Split('-')[1]);
        return $"INV-{(lastNumber + 1):D4}";
    }
}
