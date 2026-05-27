using Microsoft.EntityFrameworkCore;
using Budget1.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Budget1.Data
{
    public class BudgetDbContext : DbContext
    {
        public BudgetDbContext(DbContextOptions<BudgetDbContext> options) : base(options) { }

        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<RecurrenceInterval> RecurrenceIntervals { get; set; }
        public DbSet<Hold> Holds { get; set; }
        public DbSet<Tithe> Tithes { get; set; }
        public DbSet<SubTransaction> SubTransactions { get; set; }
        public DbSet<QuickNote> QuickNotes { get; set; }
        public DbSet<QuickNoteItem> QuickNoteItems { get; set; }
        public DbSet<QuickNoteCategory> QuickNoteCategories { get; set; }
        public DbSet<PreviousBalance> PreviousBalances { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<BudgetForecast> BudgetForecasts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v.ToUniversalTime(), // Cuando se guarda en DB
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // Cuando se lee de DB
            );

            // Aplica el converter a todas las propiedades DateTime de todas las entidades
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(DateTime));

                foreach (var property in properties)
                {
                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(dateTimeConverter);
                }
            }

            var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
                v => v.HasValue ? v.Value.ToUniversalTime() : v,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v
            );

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties()
                    .Where(p => p.PropertyType == typeof(DateTime?));

                foreach (var property in properties)
                {
                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(nullableDateTimeConverter);
                }
            }


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Wallet>().ToTable("wallets");
            modelBuilder.Entity<Category>().ToTable("categories");
            modelBuilder.Entity<Transaction>().ToTable("transactions");

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("wallets");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Balance).HasColumnName("balance");
                entity.Property(e => e.InitialBalance).HasColumnName("initial_balance");
                entity.Property(e => e.CreatedDate).HasColumnName("created_date");
                entity.Property(e => e.Currency).HasColumnName("currency").HasMaxLength(10).IsRequired();
                entity.Property(e => e.Position).HasColumnName("position");
                entity.Property(e => e.Theme).HasColumnName("theme").HasMaxLength(50).HasDefaultValue("default").IsRequired();
            });


            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transactions");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.WalletId).HasColumnName("wallet_id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Amount).HasColumnName("amount");
                entity.Property(e => e.Date).HasColumnName("date");
                entity.Property(e => e.IsIncome).HasColumnName("is_income");
                entity.Property(e => e.IsUnexpected).HasColumnName("is_unexpected");
                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                // Recurrentes
                entity.Property(e => e.IsRecurring).HasColumnName("is_recurring");
                entity.Property(e => e.RecurrenceIntervalId).HasColumnName("recurrence_interval_id");
                entity.Property(e => e.RecurrenceEndDate).HasColumnName("recurrence_end_date");
                entity.Property(e => e.RecurrenceEndType).HasColumnName("recurrence_end_type");
                entity.Property(e => e.RecurrenceCount).HasColumnName("recurrence_count");
                entity.Property(e => e.RecurrenceCount).HasColumnName("recurrence_count");
                entity.Property(e => e.OriginalTransactionId).HasColumnName("original_transaction_id");
                entity.Property(e => e.Tithe).HasColumnName("tithe");

                entity.HasOne(t => t.Wallet)
                      .WithMany(w => w.Transactions)
                      .HasForeignKey(t => t.WalletId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(t => t.Category)
                      .WithMany(c => c.Transactions)
                      .HasForeignKey(t => t.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.RecurrenceInterval)
                      .WithMany()
                      .HasForeignKey(t => t.RecurrenceIntervalId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.OriginalTransaction)
                        .WithMany()
                        .HasForeignKey(t => t.OriginalTransactionId)
                        .OnDelete(DeleteBehavior.Restrict); // o .Cascade si prefieres
            });

            modelBuilder.Entity<RecurrenceInterval>(entity =>
            {
                entity.ToTable("recurrenceinterval"); // nombre tabla en minúscula

                entity.HasKey(e => e.Id)
                    .HasName("pk_recurrenceinterval");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");
                entity.Property(e => e.DaysInterval).HasColumnName("daysinterval");
                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<MonthlyBudget>(entity =>
            {
                entity.ToTable("monthlybudgets"); // o "monthly_budgets" si prefieres snake_case

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasColumnName("year");

                entity.Property(e => e.Month)
                    .IsRequired()
                    .HasColumnName("month");

                entity.Property(e => e.BudgetAmount)
                    .IsRequired()
                    .HasColumnName("budget_amount")
                    .HasColumnType("numeric(10,2)");

                entity.Property(e => e.WalletId)
                    .IsRequired()
                    .HasColumnName("wallet_id");

                // Índice único para evitar duplicados por Wallet, Año y Mes
                entity.HasIndex(e => new { e.WalletId, e.Year, e.Month })
                    .IsUnique();

                // Relación con Wallet
                entity.HasOne(e => e.Wallet)
                    .WithMany(w => w.MonthlyBudgets)
                    .HasForeignKey(e => e.WalletId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Hold>(entity =>
            {
                entity.ToTable("hold");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.WalletId)
                    .HasColumnName("wallet_id");

                entity.Property(e => e.MonthHold)
                    .HasColumnName("month_hold");

                entity.Property(e => e.BalanceHold)
                    .HasColumnName("balance_hold")
                    .HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Wallet)
                    .WithMany(w => w.Holds)
                    .HasForeignKey(e => e.WalletId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Tithe>(entity =>
            {
                entity.ToTable("tithe");

                entity.HasKey(t => t.Id);

                entity.Property(t => t.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(t => t.Amount);

                entity.Property(t => t.Payed)
                      .IsRequired();

                entity.Property(t => t.Date)
                      .IsRequired();

                entity.HasOne(t => t.Transaction)
                      .WithMany(tr => tr.Tithes)
                      .HasForeignKey(t => t.TransactionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });


        }

    }
}
