using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessObject.Models
{
    public partial class GreenGardenContext : DbContext
    {
        public GreenGardenContext()
        {
        }

        public GreenGardenContext(DbContextOptions<GreenGardenContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; } = null!;
        public virtual DbSet<Amenity> Amenities { get; set; } = null!;
        public virtual DbSet<CampingCategory> CampingCategories { get; set; } = null!;
        public virtual DbSet<CampingGear> CampingGears { get; set; } = null!;
        public virtual DbSet<Combo> Combos { get; set; } = null!;
        public virtual DbSet<ComboCampingGearDetail> ComboCampingGearDetails { get; set; } = null!;
        public virtual DbSet<ComboFootDetail> ComboFootDetails { get; set; } = null!;
        public virtual DbSet<ComboTicketDetail> ComboTicketDetails { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<FoodAndDrink> FoodAndDrinks { get; set; } = null!;
        public virtual DbSet<FoodAndDrinkCategory> FoodAndDrinkCategories { get; set; } = null!;
        public virtual DbSet<FoodCombo> FoodCombos { get; set; } = null!;
        public virtual DbSet<FootComboItem> FootComboItems { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderCampingGearDetail> OrderCampingGearDetails { get; set; } = null!;
        public virtual DbSet<OrderComboDetail> OrderComboDetails { get; set; } = null!;
        public virtual DbSet<OrderFoodComboDetail> OrderFoodComboDetails { get; set; } = null!;
        public virtual DbSet<OrderFoodDetail> OrderFoodDetails { get; set; } = null!;
        public virtual DbSet<OrderTicketDetail> OrderTicketDetails { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Ticket> Tickets { get; set; } = null!;
        public virtual DbSet<TicketCategory> TicketCategories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=MSI\\SQL2019;database=GreenGarden;uid=sa;pwd=123;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.Property(e => e.ActivityId).HasColumnName("activity_id");

                entity.Property(e => e.ActivityName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("activity_name");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Amenity>(entity =>
            {
                entity.Property(e => e.AmenityId).HasColumnName("amenity_id");

                entity.Property(e => e.AmenityName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("amenity_name");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price")
                    .HasDefaultValueSql("((0.00))");
            });

            modelBuilder.Entity<CampingCategory>(entity =>
            {
                entity.HasKey(e => e.GearCategoryId)
                    .HasName("PK__CampingC__F8C87C83BE4F9F5A");

                entity.Property(e => e.GearCategoryId).HasColumnName("gear_category_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.GearCategoryName)
                    .HasMaxLength(100)
                    .HasColumnName("gear_category_name");
            });

            modelBuilder.Entity<CampingGear>(entity =>
            {
                entity.HasKey(e => e.GearId)
                    .HasName("PK__CampingG__82E64D3D5FA9D30D");

                entity.ToTable("CampingGear");

                entity.Property(e => e.GearId).HasColumnName("gear_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.GearCategoryId).HasColumnName("gear_category_id");

                entity.Property(e => e.GearName)
                    .HasMaxLength(100)
                    .HasColumnName("gear_name");

                entity.Property(e => e.ImgUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("img_url");

                entity.Property(e => e.QuantityAvailable).HasColumnName("quantityAvailable");

                entity.Property(e => e.RentalPrice)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("rentalPrice");

                entity.HasOne(d => d.GearCategory)
                    .WithMany(p => p.CampingGears)
                    .HasForeignKey(d => d.GearCategoryId)
                    .HasConstraintName("FK__CampingGe__gear___4222D4EF");
            });

            modelBuilder.Entity<Combo>(entity =>
            {
                entity.Property(e => e.ComboId).HasColumnName("combo_id");

                entity.Property(e => e.ComboName)
                    .HasMaxLength(100)
                    .HasColumnName("combo_name");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ImgUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("img_url");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");
            });

            modelBuilder.Entity<ComboCampingGearDetail>(entity =>
            {
                entity.HasKey(e => new { e.ComboId, e.GearId })
                    .HasName("PK__ComboCam__D0D92E7033F1A570");

                entity.Property(e => e.ComboId).HasColumnName("combo_id");

                entity.Property(e => e.GearId).HasColumnName("gear_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Combo)
                    .WithMany(p => p.ComboCampingGearDetails)
                    .HasForeignKey(d => d.ComboId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ComboCamp__combo__09A971A2");

                entity.HasOne(d => d.Gear)
                    .WithMany(p => p.ComboCampingGearDetails)
                    .HasForeignKey(d => d.GearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ComboCamp__gear___0A9D95DB");
            });

            modelBuilder.Entity<ComboFootDetail>(entity =>
            {
                entity.HasKey(e => new { e.ComboId, e.ItemId })
                    .HasName("PK__ComboFoo__DDD76A5EB2229FF7");

                entity.Property(e => e.ComboId).HasColumnName("combo_id");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Combo)
                    .WithMany(p => p.ComboFootDetails)
                    .HasForeignKey(d => d.ComboId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ComboFoot__combo__04E4BC85");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ComboFootDetails)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ComboFoot__item___05D8E0BE");
            });

            modelBuilder.Entity<ComboTicketDetail>(entity =>
            {
                entity.HasKey(e => new { e.ComboId, e.TicketId })
                    .HasName("PK__ComboTic__B5AE253508C55AE8");

                entity.Property(e => e.ComboId).HasColumnName("combo_id");

                entity.Property(e => e.TicketId).HasColumnName("ticket_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Combo)
                    .WithMany(p => p.ComboTicketDetails)
                    .HasForeignKey(d => d.ComboId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ComboTick__combo__0E6E26BF");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.ComboTicketDetails)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ComboTick__ticke__0F624AF8");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.CreateBy).HasColumnName("create_by");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.EventDate)
                    .HasColumnType("date")
                    .HasColumnName("event_date");

                entity.Property(e => e.EventName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("event_name");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("location");

                entity.Property(e => e.PictureUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("picture_url");

                entity.Property(e => e.StartTime).HasColumnName("start_time");

                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.CreateBy)
                    .HasConstraintName("FK__Events__create_b__30F848ED");
            });

            modelBuilder.Entity<FoodAndDrink>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK__FoodAndD__52020FDD3C1D54D9");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ImgUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("img_url");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(100)
                    .HasColumnName("item_name");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.QuantityAvailable).HasColumnName("quantityAvailable");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.FoodAndDrinks)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__FoodAndDr__categ__48CFD27E");
            });

            modelBuilder.Entity<FoodAndDrinkCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__FoodAndD__D54EE9B48C3EC2FF");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .HasColumnName("category_name");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(255);
            });

            modelBuilder.Entity<FoodCombo>(entity =>
            {
                entity.HasKey(e => e.ComboId)
                    .HasName("PK__FoodComb__18F74AA3B610D622");

                entity.Property(e => e.ComboId).HasColumnName("combo_id");

                entity.Property(e => e.ComboName)
                    .HasMaxLength(100)
                    .HasColumnName("combo_name");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ImgUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("img_url");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");
            });

            modelBuilder.Entity<FootComboItem>(entity =>
            {
                entity.HasKey(e => new { e.ItemId, e.ComboId })
                    .HasName("PK__FootComb__738D7B77C27F617B");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.ComboId).HasColumnName("combo_id");

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Combo)
                    .WithMany(p => p.FootComboItems)
                    .HasForeignKey(d => d.ComboId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FootCombo__combo__5070F446");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.FootComboItems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FootCombo__item___4F7CD00D");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.ActivityId).HasColumnName("activity_id");

                entity.Property(e => e.AmountPayable)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("amount_payable");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("customer_name");

                entity.Property(e => e.Deposit)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("deposit");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderUsageDate)
                    .HasColumnType("datetime")
                    .HasColumnName("order_usage_date");

                entity.Property(e => e.PhoneCustomer)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phone_customer");

                entity.Property(e => e.StatusOrder)
                    .HasColumnName("status_order")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total_amount");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ActivityId)
                    .HasConstraintName("FK__Orders__activity__66603565");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.OrderCustomers)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Orders__customer__6477ECF3");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.OrderEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Orders__employee__656C112C");
            });

            modelBuilder.Entity<OrderCampingGearDetail>(entity =>
            {
                entity.HasKey(e => new { e.GearId, e.OrderId })
                    .HasName("PK__OrderCam__0683DB1F59FFD358");

                entity.Property(e => e.GearId).HasColumnName("gear_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Gear)
                    .WithMany(p => p.OrderCampingGearDetails)
                    .HasForeignKey(d => d.GearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderCamp__gear___73BA3083");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderCampingGearDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderCamp__order__74AE54BC");
            });

            modelBuilder.Entity<OrderComboDetail>(entity =>
            {
                entity.HasKey(e => new { e.ComboId, e.OrderId })
                    .HasName("PK__OrderCom__9C92DC818A8B5E5D");

                entity.Property(e => e.ComboId).HasColumnName("combo_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Combo)
                    .WithMany(p => p.OrderComboDetails)
                    .HasForeignKey(d => d.ComboId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderComb__combo__00200768");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderComboDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderComb__order__01142BA1");
            });

            modelBuilder.Entity<OrderFoodComboDetail>(entity =>
            {
                entity.HasKey(e => new { e.ComboId, e.OrderId })
                    .HasName("PK__OrderFoo__9C92DC818897F2A1");

                entity.Property(e => e.ComboId).HasColumnName("combo_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Combo)
                    .WithMany(p => p.OrderFoodComboDetails)
                    .HasForeignKey(d => d.ComboId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderFood__combo__787EE5A0");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderFoodComboDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderFood__order__797309D9");
            });

            modelBuilder.Entity<OrderFoodDetail>(entity =>
            {
                entity.HasKey(e => new { e.ItemId, e.OrderId })
                    .HasName("PK__OrderFoo__D66799FFDD961875");

                entity.Property(e => e.ItemId).HasColumnName("item_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.OrderFoodDetails)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderFood__item___6A30C649");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderFoodDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderFood__order__6B24EA82");
            });

            modelBuilder.Entity<OrderTicketDetail>(entity =>
            {
                entity.HasKey(e => new { e.TicketId, e.OrderId })
                    .HasName("PK__OrderTic__51F36F4904989F06");

                entity.Property(e => e.TicketId).HasColumnName("ticket_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderTicketDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderTick__order__6FE99F9F");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.OrderTicketDetails)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderTick__ticke__6EF57B66");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.RoleName, "UQ__Roles__783254B16A44ECCD")
                    .IsUnique();

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.TicketId).HasColumnName("ticket_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImgUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("img_url");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.TicketCategoryId).HasColumnName("ticket_category_id");

                entity.Property(e => e.TicketName)
                    .HasMaxLength(100)
                    .HasColumnName("ticket_name");

                entity.HasOne(d => d.TicketCategory)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.TicketCategoryId)
                    .HasConstraintName("FK__Tickets__ticket___37A5467C");
            });

            modelBuilder.Entity<TicketCategory>(entity =>
            {
                entity.Property(e => e.TicketCategoryId).HasColumnName("ticket_category_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.TicketCategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("ticket_category_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.PhoneNumber, "UQ__Users__A1936A6B0CB566FE")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Users__AB6E61644EC97341")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Address)
                    .HasColumnType("text")
                    .HasColumnName("address");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("date_of_birth");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.Gender)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("gender")
                    .IsFixedLength();

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phone_number");

                entity.Property(e => e.ProfilePictureUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("profile_picture_url");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__role_id__2C3393D0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
