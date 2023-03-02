using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Entity.Models
{
    public partial class db_a956d7_bookstoredbContext : DbContext
    {
        public db_a956d7_bookstoredbContext()
        {
        }

        public db_a956d7_bookstoredbContext(DbContextOptions<db_a956d7_bookstoredbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookImage> BookImages { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ComboBook> ComboBooks { get; set; }
        public virtual DbSet<DetailComboBook> DetailComboBooks { get; set; }
        public virtual DbSet<Ebook> Ebooks { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.ImgPath).HasColumnType("ntext");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Account_Role");
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Book");

                entity.Property(e => e.Author).HasMaxLength(100);

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Isbn)
                    .HasMaxLength(20)
                    .HasColumnName("ISBN");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ReleaseYear)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Book_Category");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublisherId)
                    .HasConstraintName("FK_Book_Publisher");
            });

            modelBuilder.Entity<BookImage>(entity =>
            {
                entity.Property(e => e.ImgPath).HasColumnType("ntext");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.BookImages)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_BookImages_Book");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<ComboBook>(entity =>
            {
                entity.ToTable("ComboBook");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.PriceReduction)
                    .HasColumnType("money")
                    .HasColumnName("Price_Reduction");
            });

            modelBuilder.Entity<DetailComboBook>(entity =>
            {
                entity.ToTable("DetailComboBook");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.DetailComboBooks)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_DetailComboBook_Book");

                entity.HasOne(d => d.ComboBook)
                    .WithMany(p => p.DetailComboBooks)
                    .HasForeignKey(d => d.ComboBookId)
                    .HasConstraintName("FK_DetailComboBook_ComboBook");
            });

            modelBuilder.Entity<Ebook>(entity =>
            {
                entity.HasKey(e => e.BookId);

                entity.ToTable("Ebook");

                entity.Property(e => e.BookId).ValueGeneratedNever();

                entity.Property(e => e.EbookId).ValueGeneratedOnAdd();

                entity.Property(e => e.PdfUrl)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.HasOne(d => d.Book)
                    .WithOne(p => p.Ebook)
                    .HasForeignKey<Ebook>(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ebook_Book");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.OrderStatus).HasMaxLength(50);

                entity.Property(e => e.PaymentMethod).HasMaxLength(150);

                entity.Property(e => e.ShippingAddress).HasColumnType("ntext");

                entity.Property(e => e.TotalPrice).HasColumnType("money");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Order_Account");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.Property(e => e.EbookId).HasColumnName("EBookId");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_OrderDetail_Book");

                entity.HasOne(d => d.ComboBook)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ComboBookId)
                    .HasConstraintName("FK_OrderDetail_ComboBook");

                entity.HasOne(d => d.Ebook)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.EbookId)
                    .HasConstraintName("FK_OrderDetail_Ebook");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetail_Order");
            });

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.ToTable("Publisher");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
