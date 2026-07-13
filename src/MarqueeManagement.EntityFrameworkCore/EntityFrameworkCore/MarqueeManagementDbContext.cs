using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using MarqueeManagement.Marquees;
using MarqueeManagement.Customers;
using MarqueeManagement.MenuItems;
using MarqueeManagement.Bookings;
using MarqueeManagement.MenuCategories;
using MarqueeManagement.BookingMenuOptions;
using MarqueeManagement.FileAttachments;

namespace MarqueeManagement.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class MarqueeManagementDbContext :
    AbpDbContext<MarqueeManagementDbContext>,
    ITenantManagementDbContext,
    IIdentityDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */


    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext and ISaasDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext and ISaasDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingMenuOption> BookingMenuOptions { get; set; }
    public DbSet<MenuCategory> MenuCategories { get; set; }
    public DbSet<Marquee> Marquees { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<FileAttachment> FileAttachments { get; set; }
    public DbSet<CustomerAttachment> CustomerAttachments { get; set; }
    public MarqueeManagementDbContext(DbContextOptions<MarqueeManagementDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureTenantManagement();
        builder.ConfigureBlobStoring();

        /* Configure your own tables/entities inside here */
     
        builder.Entity<FileAttachment>(b =>
        {
            b.ToTable(
                MarqueeManagementConsts.DbTablePrefix + "FileAttachments",
                MarqueeManagementConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.FileName)
                .IsRequired()
                .HasMaxLength(FileAttachmentConsts.MaxFileNameLength);

            b.Property(x => x.BlobName)
                .IsRequired()
                .HasMaxLength(FileAttachmentConsts.MaxBlobNameLength);

            b.Property(x => x.ContentType)
                .IsRequired()
                .HasMaxLength(FileAttachmentConsts.MaxContentTypeLength);

            b.Property(x => x.Size)
                .IsRequired();
            b.Property(x => x.FileData).IsRequired();

            b.HasIndex(x => x.FileName);
        });

        builder.Entity<CustomerAttachment>(b =>
        {
            b.ToTable(
                MarqueeManagementConsts.DbTablePrefix + "CustomerAttachments",
                MarqueeManagementConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.TenantId);

            b.Property(x => x.CustomerId)
                .IsRequired();

            b.Property(x => x.FileAttachmentId)
                .IsRequired();

            b.HasIndex(x => x.CustomerId);
            b.HasIndex(x => x.FileAttachmentId);

            b.HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            b.HasOne(x => x.FileAttachment)
                .WithMany()
                .HasForeignKey(x => x.FileAttachmentId)
                .IsRequired();
        });


        builder.Entity<Booking>(b =>
        {
            b.ToTable(MarqueeManagementConsts.DbTablePrefix + "Bookings",
                      MarqueeManagementConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.EventDate)
                .IsRequired();
            b.HasIndex(x => x.EventDate);

            b.Property(x => x.EventType)
                .IsRequired();
            b.HasIndex(x => x.EventType);

            b.Property(x => x.GuestCount)
                .IsRequired();
            b.HasIndex(x => x.GuestCount);

            b.Property(x => x.TotalAmount)
                .IsRequired();
            b.HasIndex(x => x.TotalAmount);

            b.Property(x => x.Status)
                .IsRequired();
            b.HasIndex(x => x.Status);

            b.HasOne(x => x.Marquee)
             .WithMany(x => x.Bookings)
             .HasForeignKey(x => x.MarqueeId)
             .IsRequired();

            b.HasOne(x => x.Customer)
             .WithMany(x => x.Bookings)
             .HasForeignKey(x => x.CustomerId)
             .IsRequired();
        });

        builder.Entity<MenuCategory>(b =>
        {
            b.ToTable(MarqueeManagementConsts.DbTablePrefix + "MenuCategories",
                      MarqueeManagementConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(MenuCategoryConsts.MaxNameLength);
            b.HasIndex(x => x.Name);

            b.Property(x => x.Description)
                .HasMaxLength(MenuCategoryConsts.MaxDescriptionLength);
            b.HasIndex(x => x.Description);
        });

        builder.Entity<Marquee>(b =>
        {
            b.ToTable(MarqueeManagementConsts.DbTablePrefix + "Marquees",
               MarqueeManagementConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired()
            .HasMaxLength(MarqueeConsts.MaxNameLength);
            b.HasIndex(x => x.Name);
            b.Property(x => x.Location).IsRequired().HasMaxLength(MarqueeConsts.MaxLocationLength);
            b.HasIndex(x => x.Location);
            b.Property(x => x.Description);
            b.HasIndex(x => x.Description);
            b.Property(x => x.Capacity).IsRequired();
            b.HasIndex(x => x.Capacity);
            b.Property(x => x.PricePerDay).IsRequired();
            b.HasIndex(x => x.PricePerDay);
        });

        builder.Entity<Customer>(b =>
        {
            b.ToTable(
                MarqueeManagementConsts.DbTablePrefix + "Customers",
                MarqueeManagementConsts.DbSchema
            );

            b.ConfigureByConvention();
            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(CustomerConsts.MaxNameLength);

            b.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(CustomerConsts.MaxPhoneLength);

            b.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(CustomerConsts.MaxEmailLength);

            b.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(CustomerConsts.MaxAddressLength);

            b.HasIndex(x => x.Phone);
            b.HasIndex(x => x.Email);
        });

        builder.Entity<MenuItem>(b =>
        {
            b.ToTable(
                MarqueeManagementConsts.DbTablePrefix + "MenuItems",
                MarqueeManagementConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(MenuItemConsts.MaxNameLength);

            b.Property(x => x.Description)
                .HasMaxLength(MenuItemConsts.MaxDescriptionLength);

            b.Property(x => x.Price)
                .IsRequired();

            b.Property(x => x.IsAvailable).IsRequired();

            b.HasIndex(x => x.Name);
            b.HasIndex(x => x.Price);
            b.HasOne(menuItem => menuItem.MenuCategory)
              .WithMany(menuCategory => menuCategory.MenuItems)
              .HasForeignKey(menuItem => menuItem.MenuCategoryId)
              .IsRequired();
            b.Property(x => x.ImageUrl)
            .HasMaxLength(500);
            //b.HasOne<MenuCategory>()
            //.WithMany()
            //.HasForeignKey(x => x.MenuCategoryId);
        });


        builder.Entity<BookingMenuOption>(b =>
        {
            b.ToTable(MarqueeManagementConsts.DbTablePrefix + "BookingMenuOptions",
               MarqueeManagementConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(x => x.Quantity).IsRequired();
            b.HasIndex(x => x.Quantity);
            b.Property(x => x.PriceAtBookingTime).IsRequired();
            b.HasIndex(x => x.PriceAtBookingTime);

            b.HasOne(x => x.Booking)
             .WithMany(x => x.BookingMenuOptions)
             .HasForeignKey(x => x.BookingId)
             .IsRequired();

            b.HasOne(x => x.MenuItem)
             .WithMany(x => x.BookingMenuOptions)
             .HasForeignKey(x => x.MenuItemId)
             .IsRequired();
        });

    }
}
