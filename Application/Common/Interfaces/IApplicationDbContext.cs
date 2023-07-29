using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Domain.Entities.BillingAddress> BillingAddress { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Domain.Entities.ShippingAddress> ShippingAddress { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<DetailOrder> DetailOrder { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
