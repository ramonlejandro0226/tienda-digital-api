using api_tienda_digital.Context.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api_tienda_digital.Context;

public partial class TiendaDigitalContext : IdentityDbContext
{
    public TiendaDigitalContext(DbContextOptions<TiendaDigitalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderDetail> OrderDetails { get; set; }
    public virtual DbSet<OrderProduct> OrderProducts { get; set; }
    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Status> Status { get; set; }
}
