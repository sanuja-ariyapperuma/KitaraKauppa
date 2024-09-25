using System;
using System.Collections.Generic;
using KitaraKauppa.Core.Users;
using KitaraKauppa.Core.ProductReviews;
using KitaraKauppa.Core.Orders;
using KitaraKauppa.Core.Carts;
using Microsoft.EntityFrameworkCore;
using KitaraKauppa.Core.Products;
using KitaraKauppa.Core.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KitaraKauppa.Infrastrcture.Database;

public partial class KitaraKauppaDbContext : DbContext
{
    public KitaraKauppaDbContext() : base()
    {}

    public KitaraKauppaDbContext(DbContextOptions<KitaraKauppaDbContext> options)
        : base(options)
    {
    }

    // User Entites
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserAddress> UserAddresses { get; set; }
    public virtual DbSet<UserContactNumber> UserContactNumbers { get; set; }
    public virtual DbSet<UserCredential> UserCredentials { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }
    public virtual DbSet<City> Cities { get; set; }

    //Product Entities
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Brand> Brands { get; set; }
    public virtual DbSet<Image> Images { get; set; }
    public virtual DbSet<Color> Colors { get; set; }

    // Order Entities
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entity in modelBuilder.Model.GetEntityTypes()) 
        {
            entity.SetTableName(entity.GetTableName()!.ToLower());

            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.GetColumnName().ToLower());
            }
        }

        //Users

        modelBuilder.Entity<User>(entity =>
        {
            ConfigureCommonProperties(entity);
            
            entity.HasOne(e => e.UserCredential)
            .WithOne(e => e.User)
            .HasForeignKey<UserCredential>(e => e.UserId)
            .IsRequired();
        });
        modelBuilder.Entity<UserAddress>(entity =>
        {
            ConfigureCommonProperties(entity);
        });
        modelBuilder.Entity<UserContactNumber>(entity =>
        {
            ConfigureCommonProperties(entity);
        });
        modelBuilder.Entity<UserCredential>(entity =>
        {
            ConfigureCommonProperties(entity);
            entity.HasIndex(e => e.UserName).IsUnique();
        });
        modelBuilder.Entity<UserRole>(entity =>
        {
            ConfigureCommonProperties(entity);
        });
        modelBuilder.Entity<City>(entity =>
        {
            ConfigureCommonProperties(entity);
        });

        //Product

        modelBuilder.Entity<Product>(entity =>
        {
            ConfigureCommonProperties(entity);

            entity.Property(u => u.VarientType)
            .HasConversion<string>();

            entity.Property(u => u.Orientation)
            .HasConversion<string>();

        });
        modelBuilder.Entity<Brand>(entity => 
        {
            ConfigureCommonProperties(entity);
        });

        modelBuilder.Entity<Image>(entity =>
        {
            ConfigureCommonProperties(entity);
        });
        modelBuilder.Entity<Color>(entity =>
        {
            ConfigureCommonProperties(entity);
        });

        //Order

        modelBuilder.Entity<Order>(entity =>
        {
            ConfigureCommonProperties(entity);
            
            entity.Property(e => e.OrderStatus)
            .HasConversion<string>();
        });
        modelBuilder.Entity<OrderItem>(entity =>
        {
            ConfigureCommonProperties(entity);
            entity.ToTable(e => e.HasCheckConstraint("CK_OrderItem_Units", "\"units\" > 0"));

            entity.Property(e => e.Orientation)
            .HasConversion<string>();
        });


        modelBuilder.Entity<City>().HasData(
            new City { Id = Guid.Parse("1f819ac4-52a1-4c27-ad03-3ba21159e85a"), CityName = "Helsinki" },
            new City { Id = Guid.Parse("0dfb4d4e-f694-4101-b0e6-e04c73c40584"), CityName = "Espoo" },
            new City { Id = Guid.Parse("99950502-4beb-45a0-ab95-5285f0e9a517"), CityName = "Vantaa" },
            new City { Id = Guid.Parse("ac0a350a-fdff-46d0-9839-e2057a81d62a"), CityName = "Tampere" },
            new City { Id = Guid.Parse("b423dcc6-00d2-4a9a-871d-e8900fd48c97"), CityName = "Turku" },
            new City { Id = Guid.Parse("62046a0d-177f-4611-869b-cc14ff44ecef"), CityName = "Oulu" }
        );

        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { Id = Guid.Parse("c2a1c420-c068-48b5-854c-ffb247904891"), UserRoleName = "Admin" },
            new UserRole { Id = Guid.Parse("e08e5d7f-8d13-4e07-8ed0-29268b1a59d8"), UserRoleName = "Customer" }
        );

        modelBuilder.Entity<Brand>().HasData(
            new Brand { Id = Guid.Parse("d38c4b9e-b13b-4aee-ad44-dd55db7e3919"), Name = "Fender" },
            new Brand { Id =  Guid.Parse("8996e7b6-755c-4cf8-8512-294c7916bdd4"), Name = "Gibson" },
            new Brand { Id =  Guid.Parse("81a253d0-a07c-4cd5-a838-a200279cba3f"), Name = "Ibanez" },
            new Brand { Id =  Guid.Parse("d95ee07d-f51a-4322-b4ce-b1bdbf4443cb"), Name = "Martin" },
            new Brand { Id =  Guid.Parse("7e95231a-ed06-440a-b561-d8c2d8d2f97e"), Name = "PRS" },
            new Brand { Id =  Guid.Parse("53479238-506e-4abd-940c-449bddb101fa"), Name = "Sigma" },
            new Brand { Id =  Guid.Parse("4a02d3b7-c66d-4510-9ab5-191800cc3d49"), Name = "Takamine" },
            new Brand { Id =  Guid.Parse("74074506-e224-4d37-8286-308958a125cd"), Name = "Yamaha" }
        );

        modelBuilder.Entity<Color>().HasData(
            new Color { Id = Guid.Parse("32c1c9ba-bcfb-41bd-a1d5-12def60bc714"), Name = "Black" },
            new Color { Id = Guid.Parse("214ee9ef-c53a-4021-8378-fcbd67ab0cf0"), Name = "White" },
            new Color { Id = Guid.Parse("5ac0cc75-623b-4968-a4f7-aeb38100077a"), Name = "Red" },
            new Color { Id = Guid.Parse("85e730db-f1a1-47a8-b8c7-ffe52f1a511e"), Name = "Green" },
            new Color { Id = Guid.Parse("d66fd662-8176-4b20-bbb1-0b4844a271d1"), Name = "Blue" },
            new Color { Id = Guid.Parse("7fdc99fb-0738-4eab-9692-93b3848a4f10"), Name = "Yellow" }
            );

        modelBuilder.Entity<Product>().HasData(
            new Product { 
                Id = Guid.Parse("25435b04-9a85-4577-be4b-8816c287810c"), 
                Title = "Fender Stratocaster", 
                Description = "The Fender Stratocaster is a model of electric guitar designed from 1952 into 1954 by Leo Fender, Bill Carson, George Fullerton and Freddie Tavares. The Fender Musical Instruments Corporation has continuously manufactured the Stratocaster from 1954 to the present. It is a double-cutaway guitar, with an extended top horn shape for balance. Along with the Gibson Les Paul and Fender Telecaster, it is one of the most-often emulated electric guitar shapes.", 
                BrandId = Guid.Parse("d38c4b9e-b13b-4aee-ad44-dd55db7e3919"), 
                UnitPrice = 1000,
                Orientation = Orientation.RightHanded,
                CreatedAt = DateTime.UtcNow, 
                UpdatedAt = DateTime.UtcNow,
                VarientType = VarientType.Electric
            },
            new Product
            {
                Id = Guid.Parse("275b7cb3-df3d-4d11-bbc1-c0664e270f25"),
                Title = "Gibson Les Paul",
                Description = "The Gibson Les Paul is a solid body electric guitar that was first sold by the Gibson Guitar Corporation in 1952. The Les Paul was designed by Gibson president Ted McCarty, factory manager John Huis and their team with input from and endorsement by guitarist Les Paul. Its design typically comprises a solid mahogany body with a carved maple top and a single cutaway, a mahogany set-in neck with a rosewood fretboard, two pickups with independent volume and tone controls, and a stoptail bridge, although variants exist.",
                BrandId = Guid.Parse("8996e7b6-755c-4cf8-8512-294c7916bdd4"),
                UnitPrice = 1500,
                Orientation = Orientation.LeftHanded,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                VarientType = VarientType.Electric
            },
            new Product
            {
                Id = Guid.Parse("6e7fe926-0695-4b92-9e5d-f26fb0bfdead"),
                Title = "Ibanez RG",
                Description = "The Ibanez RG is a series of electric guitars produced by Hoshino Gakki and one of the best-selling superstrat-style hard rock/heavy metal guitars ever made. The first in the series, RG550, was originally released in 1987 and advertised as part of the Roadstar series. The RG series is a line of solid body electric guitars produced by Hoshino Gakki and sold under the Ibanez brand. The series was introduced in 1987 as the Roadstar (RG) series and expanded in 1992.",
                BrandId = Guid.Parse("81a253d0-a07c-4cd5-a838-a200279cba3f"),
                UnitPrice = 800,
                Orientation = Orientation.BothOptions,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                VarientType = VarientType.Electric
            },
            new Product
            {
                Id = Guid.Parse("bb5abee4-5507-48c6-a82d-e432967c5141"),
                Title = "Martin D-28",
                Description = "The Martin D-28 is a dreadnought-style acoustic guitar made by C. F. Martin & Company of Nazareth, Pennsylvania. It is widely regarded as the instrument that set the standard for the dreadnought guitar style. The D-28 is the standard by which many steel-string guitars are measured and is often used as a reference for the sound of acoustic guitars.",
                BrandId = Guid.Parse("d95ee07d-f51a-4322-b4ce-b1bdbf4443cb"),
                UnitPrice = 2000,
                Orientation = Orientation.RightHanded,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                VarientType = VarientType.Accoustic
            },
            new Product
            {
                Id = Guid.Parse("e8df56e4-d3b6-4587-9102-160651f476f8"),
                Title = "PRS Custom 24",
                Description = "The PRS Custom 24 is the original PRS—the guitar Paul Reed Smith took to his first tradeshow in 1985. A perennial favorite with musicians, this model features a patented PRS tremolo system, PRS Phase III locking tuners, and a pair of humbucking pickups. The Custom 24 is the original PRS. Since its introduction, it has offered a unique tonal option for serious players and defined PRS tone and playability.",
                BrandId = Guid.Parse("7e95231a-ed06-440a-b561-d8c2d8d2f97e"),
                UnitPrice = 2500,
                Orientation = Orientation.LeftHanded,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                VarientType = VarientType.Electric
            },
            new Product
            {
                Id = Guid.Parse("1edf02b3-9dd3-4625-aebc-326badf35cbc"),
                Title = "Sigma 000M-15",
                Description = "The Sigma 000M-15 is a 000 body size acoustic guitar with a solid mahogany top, back and sides. The 000M-15 is a 000 body size acoustic guitar with a solid mahogany top, back and sides. The 000M-15 is a 000 body size acoustic guitar with a solid mahogany top, back and sides.",
                BrandId = Guid.Parse("53479238-506e-4abd-940c-449bddb101fa"),
                UnitPrice = 500,
                Orientation = Orientation.BothOptions,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                VarientType = VarientType.Accoustic
            },
            new Product
            {
                Id = Guid.Parse("d81feab3-b560-44e6-b31b-704e08f5a591"),
                Title = "Takamine GD20-NS",
                Description = "The Takamine GD20-NS is a dreadnought acoustic guitar with a solid cedar top and mahogany back and sides. The GD20-NS is a dreadnought acoustic guitar with a solid cedar top and mahogany back and sides. The GD20-NS is a dreadnought acoustic guitar with a solid cedar top and mahogany back and sides.",
                BrandId = Guid.Parse("4a02d3b7-c66d-4510-9ab5-191800cc3d49"),
                UnitPrice = 600,
                Orientation = Orientation.RightHanded,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                VarientType = VarientType.Accoustic
            },
            new Product
            {
                Id = Guid.Parse("a73368de-2bb9-4a1a-a08f-5b451f7a5515"),
                Title = "Yamaha FG800",
                Description = "The Yamaha FG800 is a dreadnought acoustic guitar with a solid spruce top and nato back and sides. The FG800 is a dreadnought acoustic guitar with a solid spruce top and nato back and sides. The FG800 is a dreadnought acoustic guitar with a solid spruce top and nato back and sides.",
                BrandId = Guid.Parse("74074506-e224-4d37-8286-308958a125cd"),
                UnitPrice = 400,
                Orientation = Orientation.LeftHanded,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                VarientType = VarientType.Accoustic
            });

        modelBuilder.Entity<Product>()
            .HasMany(s => s.Colors)
            .WithMany(c => c.Products)
            .UsingEntity(j => j.HasData(
                    new { ProductsId = Guid.Parse("25435b04-9a85-4577-be4b-8816c287810c"), ColorsId = Guid.Parse("32c1c9ba-bcfb-41bd-a1d5-12def60bc714") },
                    new { ProductsId = Guid.Parse("25435b04-9a85-4577-be4b-8816c287810c"), ColorsId = Guid.Parse("214ee9ef-c53a-4021-8378-fcbd67ab0cf0") },
                    new { ProductsId = Guid.Parse("275b7cb3-df3d-4d11-bbc1-c0664e270f25"), ColorsId = Guid.Parse("214ee9ef-c53a-4021-8378-fcbd67ab0cf0") },
                    new { ProductsId = Guid.Parse("275b7cb3-df3d-4d11-bbc1-c0664e270f25"), ColorsId = Guid.Parse("5ac0cc75-623b-4968-a4f7-aeb38100077a") },
                    new { ProductsId = Guid.Parse("6e7fe926-0695-4b92-9e5d-f26fb0bfdead"), ColorsId = Guid.Parse("5ac0cc75-623b-4968-a4f7-aeb38100077a") },
                    new { ProductsId = Guid.Parse("6e7fe926-0695-4b92-9e5d-f26fb0bfdead"), ColorsId = Guid.Parse("85e730db-f1a1-47a8-b8c7-ffe52f1a511e") },
                    new { ProductsId = Guid.Parse("bb5abee4-5507-48c6-a82d-e432967c5141"), ColorsId = Guid.Parse("85e730db-f1a1-47a8-b8c7-ffe52f1a511e") },
                    new { ProductsId = Guid.Parse("bb5abee4-5507-48c6-a82d-e432967c5141"), ColorsId = Guid.Parse("32c1c9ba-bcfb-41bd-a1d5-12def60bc714") },
                    new { ProductsId = Guid.Parse("e8df56e4-d3b6-4587-9102-160651f476f8"), ColorsId = Guid.Parse("d66fd662-8176-4b20-bbb1-0b4844a271d1") },
                    new { ProductsId = Guid.Parse("e8df56e4-d3b6-4587-9102-160651f476f8"), ColorsId = Guid.Parse("7fdc99fb-0738-4eab-9692-93b3848a4f10") },
                    new { ProductsId = Guid.Parse("1edf02b3-9dd3-4625-aebc-326badf35cbc"), ColorsId = Guid.Parse("7fdc99fb-0738-4eab-9692-93b3848a4f10") },
                    new { ProductsId = Guid.Parse("1edf02b3-9dd3-4625-aebc-326badf35cbc"), ColorsId = Guid.Parse("5ac0cc75-623b-4968-a4f7-aeb38100077a") },
                    new { ProductsId = Guid.Parse("d81feab3-b560-44e6-b31b-704e08f5a591"), ColorsId = Guid.Parse("32c1c9ba-bcfb-41bd-a1d5-12def60bc714") },
                    new { ProductsId = Guid.Parse("d81feab3-b560-44e6-b31b-704e08f5a591"), ColorsId = Guid.Parse("214ee9ef-c53a-4021-8378-fcbd67ab0cf0") },
                    new { ProductsId = Guid.Parse("a73368de-2bb9-4a1a-a08f-5b451f7a5515"), ColorsId = Guid.Parse("214ee9ef-c53a-4021-8378-fcbd67ab0cf0") },
                    new { ProductsId = Guid.Parse("a73368de-2bb9-4a1a-a08f-5b451f7a5515"), ColorsId = Guid.Parse("5ac0cc75-623b-4968-a4f7-aeb38100077a") }
                ));
            

    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>()) 
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            } else if (entry.State == EntityState.Modified) 
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>()) 
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            var connectionString = configuration.GetConnectionString("MigrationConnection");
            optionsBuilder.UseNpgsql(connectionString);
        }
    }


    // public virtual DbSet<AddressDetail> AddressDetails { get; set; }

    // public virtual DbSet<Cart> Carts { get; set; }

    // public virtual DbSet<CartItem> CartItems { get; set; }

    // public virtual DbSet<Category> Categories { get; set; }

    // public virtual DbSet<City> Cities { get; set; }

    // public virtual DbSet<Image> Images { get; set; }

    // public virtual DbSet<Order> Orders { get; set; }

    // public virtual DbSet<OrderItem> OrderItems { get; set; }

    // public virtual DbSet<Product> Products { get; set; }

    // public virtual DbSet<ProductReview> ProductReviews { get; set; }

    // public virtual DbSet<ProductCategory> ProductsCategories { get; set; }

    // public virtual DbSet<User> Users { get; set; }

    // public virtual DbSet<UserAddress> UserAddresses { get; set; }

    // public virtual DbSet<UserContactNumber> UserContactNumbers { get; set; }

    // public virtual DbSet<UserCredential> UserCredentials { get; set; }

    // public virtual DbSet<UserRole> UserRoles { get; set; }

    // public virtual DbSet<Variation> Variations { get; set; }

    // From Here
    //public virtual DbSet<AddressDetail> AddressDetails { get; set; }

    //public virtual DbSet<City> Cities { get; set; }

    //public virtual DbSet<User> Users { get; set; }

    //public virtual DbSet<UserAddress> UserAddresses { get; set; }

    //public virtual DbSet<UserContactNumber> UserContactNumbers { get; set; }

    //public virtual DbSet<UserCredential> UserCredentials { get; set; }

    //public virtual DbSet<UserRole> UserRoles { get; set; }

    //// product related tables
    //public virtual DbSet<Category> Categories { get; set; }
    //public virtual DbSet<Image> Images { get; set; }
    //public virtual DbSet<Product> Products { get; set; }
    //public virtual DbSet<Variation> Variations { get; set; }
    //public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    //// order related tables
    //public virtual DbSet<Order> Orders { get; set; }
    //public virtual DbSet<OrderItem> OrderItems { get; set; }
    //public virtual DbSet<Cart> Carts { get; set; }
    //public virtual DbSet<CartItem> CartItems { get; set; }
    //public virtual DbSet<ProductReview> ProductReviews { get; set; }

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    modelBuilder.HasPostgresExtension("pg_trgm");

    //    modelBuilder.Entity<AddressDetail>(entity =>
    //    {
    //        entity.HasKey(e => e.AddressId).HasName("address_details_pkey");

    //        entity.ToTable("address_details");

    //        entity.Property(e => e.AddressId)
    //            .HasDefaultValueSql("gen_random_uuid()")
    //            .HasColumnName("address_id");
    //        entity.Property(e => e.AddressLine1)
    //            .HasMaxLength(100)
    //            .HasColumnName("address_line_1");
    //        entity.Property(e => e.AddressLine2)
    //            .HasMaxLength(100)
    //            .HasColumnName("address_line_2");
    //        entity.Property(e => e.CityId).HasColumnName("city_id");

    //        entity.HasOne(d => d.City).WithMany(p => p.AddressDetails)
    //            .HasForeignKey(d => d.CityId)
    //            .OnDelete(DeleteBehavior.Restrict)
    //            .HasConstraintName("address_details_city_id_fkey");
    //    });

    //    modelBuilder.Entity<City>(entity =>
    //    {
    //        entity.HasKey(e => e.CityId).HasName("cities_pkey");

    //        entity.ToTable("cities");

    //        entity.Property(e => e.CityId)
    //            .HasDefaultValueSql("gen_random_uuid()")
    //            .HasColumnName("city_id");
    //        entity.Property(e => e.CityName)
    //            .HasMaxLength(50)
    //            .HasColumnName("city_name");
    //    });

    //    modelBuilder.Entity<User>(entity =>
    //    {
    //        entity.HasKey(e => e.UserId).HasName("users_pkey");

    //        entity.ToTable("users");

    //        entity.Property(e => e.UserId)
    //            .HasDefaultValueSql("gen_random_uuid()")
    //            .HasColumnName("user_id");
    //        entity.Property(e => e.CreatedAt)
    //            .HasDefaultValueSql("CURRENT_TIMESTAMP")
    //            .HasColumnType("timestamp without time zone")
    //            .HasColumnName("created_at");
    //        entity.Property(e => e.Email)
    //            .HasMaxLength(100)
    //            .HasColumnName("email");
    //        entity.Property(e => e.FirstName)
    //            .HasMaxLength(50)
    //            .HasColumnName("first_name");
    //        entity.Property(e => e.IsUserActive)
    //            .HasDefaultValue(true)
    //            .HasColumnName("is_user_active");
    //        entity.Property(e => e.LastLogin)
    //            .HasColumnType("timestamp without time zone")
    //            .HasColumnName("last_login");
    //        entity.Property(e => e.LastName)
    //            .HasMaxLength(50)
    //            .HasColumnName("last_name");
    //        entity.Property(e => e.UserRoleId).HasColumnName("user_role_id");

    //        entity.HasOne(d => d.UserRole).WithMany(p => p.Users)
    //            .HasForeignKey(d => d.UserRoleId)
    //            .OnDelete(DeleteBehavior.Restrict)
    //            .HasConstraintName("users_user_role_id_fkey");

    //        entity.HasOne(d => d.UserCredential).WithOne(p => p.User);
    //    });

    //    modelBuilder.Entity<UserAddress>(entity =>
    //    {
    //        entity.HasKey(e => new { e.UserId, e.AddressId }).HasName("user_addresses_pkey");

    //        entity.ToTable("user_addresses");

    //        entity.Property(e => e.UserId).HasColumnName("user_id");
    //        entity.Property(e => e.AddressId).HasColumnName("address_id");
    //        entity.Property(e => e.DefaultAddress)
    //            .HasDefaultValue(false)
    //            .HasColumnName("default_address");

    //        entity.HasOne(d => d.Address).WithMany(p => p.UserAddresses)
    //            .HasForeignKey(d => d.AddressId)
    //            .OnDelete(DeleteBehavior.Restrict)
    //            .HasConstraintName("user_addresses_address_id_fkey");

    //        entity.HasOne(d => d.User).WithMany(p => p.UserAddresses)
    //            .HasForeignKey(d => d.UserId)
    //            .OnDelete(DeleteBehavior.Restrict)
    //            .HasConstraintName("user_addresses_user_id_fkey");
    //    });

    //    modelBuilder.Entity<UserContactNumber>(entity =>
    //    {
    //        entity.HasKey(e => new { e.UserId, e.ContactNumber }).HasName("user_contact_number_pkey");

    //        entity.ToTable("user_contact_number");

    //        entity.Property(e => e.UserId).HasColumnName("user_id");
    //        entity.Property(e => e.ContactNumber)
    //            .HasMaxLength(10)
    //            .HasColumnName("contact_number");

    //        entity.HasOne(d => d.User).WithMany(p => p.UserContactNumbers)
    //            .HasForeignKey(d => d.UserId)
    //            .OnDelete(DeleteBehavior.Restrict)
    //            .HasConstraintName("user_contact_number_user_id_fkey");
    //    });

    //    modelBuilder.Entity<UserCredential>(entity =>
    //    {
    //        entity.HasKey(e => e.UserId).HasName("user_credentials_pk");

    //        entity.Property(e => e.Password).HasColumnName("password");
    //        entity.Property(e => e.UserId).HasColumnName("user_id");
    //        entity.Property(e => e.UserName)
    //            .HasMaxLength(50)
    //            .HasColumnName("user_name");

    //        entity.HasOne(d => d.User).WithOne(p => p.UserCredential)
    //            .OnDelete(DeleteBehavior.Restrict)
    //            .HasConstraintName("user_credentials_user_id_fkey");
    //    });

    //    modelBuilder.Entity<UserRole>(entity =>
    //    {
    //        entity.HasKey(e => e.UserRoleId).HasName("user_roles_pkey");

    //        entity.ToTable("user_roles");

    //        entity.Property(e => e.UserRoleId)
    //            .HasDefaultValueSql("gen_random_uuid()")
    //            .HasColumnName("user_role_id");
    //        entity.Property(e => e.UserRoleName)
    //            .HasMaxLength(20)
    //            .HasColumnName("user_role_name");
    //    });

    //    OnModelCreatingPartial(modelBuilder);
    //}

    //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    private void ConfigureCommonProperties<TEntity>(EntityTypeBuilder<TEntity> entity) where TEntity : BaseEntity
    {
        entity.Property(e => e.CreatedAt)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.Property(e => e.UpdatedAt)
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.Property(e => e.IsDeleted)
            .HasDefaultValue(false);

        entity.HasQueryFilter(e => !e.IsDeleted);
    }
}