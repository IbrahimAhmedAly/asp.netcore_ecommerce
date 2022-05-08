using System;
using Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
//using Domains;
#nullable disable

namespace book.Models
{
    public partial class StoreWebsiteContext : IdentityDbContext<IdentityUser>
    {
        public StoreWebsiteContext()
        {
        }

        public StoreWebsiteContext(DbContextOptions<StoreWebsiteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CommonCode> CommonCodes { get; set; }
        public virtual DbSet<TbCashTransaction> TbCashTransactions { get; set; }
        public virtual DbSet<TbCategory> TbCategories { get; set; }
        public virtual DbSet<TbCustomer> TbCustomers { get; set; }
        public virtual DbSet<TbItem> TbItems { get; set; }
        public virtual DbSet<TbItemImage> TbItemImages { get; set; }
        public virtual DbSet<TbPurchaseInvoice> TbPurchaseInvoices { get; set; }
        public virtual DbSet<TbPurchaseInvoiceItem> TbPurchaseInvoiceItems { get; set; }
        public virtual DbSet<TbSalesInvoice> TbSalesInvoices { get; set; }
        public virtual DbSet<TbSalesInvoiceItem> TbSalesInvoiceItems { get; set; }
        public virtual DbSet<Tbsupplier> Tbsuppliers { get; set; }
        public virtual DbSet<TbSlider> TbSliders { get; set; }
        public virtual DbSet<TbItemDiscount> TbItemDiscount { get; set; }
        public virtual DbSet<View1> View1s { get; set; }
        public virtual DbSet<VwItemCategory> VwItemCategories { get; set; }
        public virtual DbSet<VwItemsOutOfInvoice> VwItemsOutOfInvoices { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-H7NTH2S\\SQLEXPRESS;Database=StoreWebsite;Trusted_Connection=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CommonCode>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CommonCode");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ItemName)
                    .HasMaxLength(100)
                    .HasColumnName("ITemName");
            });

            modelBuilder.Entity<TbCashTransaction>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TbCashTransaction");

                entity.Property(e => e.CashDate).HasColumnType("datetime");

                entity.Property(e => e.CashValue).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<TbCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK_TBCategories");

                entity.Property(e => e.CategoryId).ValueGeneratedNever();

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TbCustomer>(entity =>
            {
                entity.HasKey(e => e.CustomerId)
                    .HasName("PK_TBCustomers");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TbItem>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK_TBItems");

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("ITemName");

                entity.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 4)");

                entity.Property(e => e.SalesPrice).HasColumnType("decimal(8, 4)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TbItems)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbItems_TbCategories");
            });

            modelBuilder.Entity<TbItemImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.Property(e => e.ImageName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.TbItemImages)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbItemImages_TbItems");
            });
            modelBuilder.Entity<TbItemDiscount>(entity =>
            {
                 entity.HasOne(d => d.Item)
                    .WithMany(p => p.TbItemDiscount)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbItemDiscount_TbItems");
            });
            modelBuilder.Entity<TbPurchaseInvoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceId);

                entity.Property(e => e.InvoiceDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.TbPurchaseInvoices)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbPurchaseInvoices_Tbsupplier");
            });

            modelBuilder.Entity<TbPurchaseInvoiceItem>(entity =>
            {
                entity.HasKey(e => e.InvoiceItemId);

                entity.Property(e => e.InvoicePrice).HasColumnType("decimal(8, 4)");

                entity.Property(e => e.Qty).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.TbPurchaseInvoiceItems)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbPurchaseInvoiceItems_TbPurchaseInvoices");

                entity.HasOne(d => d.InvoiceNavigation)
                    .WithMany(p => p.TbPurchaseInvoiceItems)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbPurchaseInvoiceItems_TbItems");
            });

            modelBuilder.Entity<TbSalesInvoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceId);

                entity.Property(e => e.DeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.InvoiceDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TbSalesInvoices)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbSalesInvoices_TbCustomers");
            });

            modelBuilder.Entity<TbSalesInvoiceItem>(entity =>
            {
                entity.HasKey(e => e.InvoiceItemId);

                entity.Property(e => e.InvoicePrice).HasColumnType("decimal(8, 4)");

                entity.Property(e => e.Qty).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.TbSalesInvoiceItems)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbSalesInvoiceItems_TbSalesInvoices");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.TbSalesInvoiceItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbSalesInvoiceItems_TbItems");
            });

            modelBuilder.Entity<Tbsupplier>(entity =>
            {
                entity.HasKey(e => e.SupplierId)
                    .HasName("PK_TBSupplier");

                entity.ToTable("Tbsupplier");

                entity.Property(e => e.SupplierName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<View1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_1");

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("ITemName");
            });

            modelBuilder.Entity<VwItemCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwItemCategories");

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("ITemName");

                entity.Property(e => e.SalesPrice).HasColumnType("decimal(8, 4)");
            });

            modelBuilder.Entity<VwItemsOutOfInvoice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwItemsOutOfInvoices");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.InvoicePrice).HasColumnType("decimal(8, 4)");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(100)
                    .HasColumnName("ITemName");

                entity.Property(e => e.PurchasePrice).HasColumnType("decimal(8, 4)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
