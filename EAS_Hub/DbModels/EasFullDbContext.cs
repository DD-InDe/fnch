using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace EAS_Hub.DbModels;

public partial class EasFullDbContext : DbContext
{
    public EasFullDbContext()
    {
    }

    public EasFullDbContext(DbContextOptions<EasFullDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdaptationMap> AdaptationMaps { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<MapModule> MapModules { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<ModuleAccess> ModuleAccesses { get; set; }

    public virtual DbSet<ModuleStatus> ModuleStatuses { get; set; }

    public virtual DbSet<ModuleWork> ModuleWorks { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Testing> Testings { get; set; }

    public virtual DbSet<TestingResult> TestingResults { get; set; }

    public virtual DbSet<TestingType> TestingTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LEGION\\DEERSERVER;Database=EAS_Full_Db;Trusted_connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdaptationMap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Adaptati__3214EC070D68EC25");

            entity.ToTable("AdaptationMap");

            entity.Property(e => e.DateCreate).HasColumnType("datetime");

            entity.HasOne(d => d.Department).WithMany(p => p.AdaptationMaps)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("AdaptationMap___fk_Department");

            entity.HasOne(d => d.Employee).WithMany(p => p.AdaptationMaps)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Adaptatio__Emplo__440B1D61");

            entity.HasOne(d => d.Position).WithMany(p => p.AdaptationMaps)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("AdaptationMap___fk_Position");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC079133C078");

            entity.ToTable("Department");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC073EC684FD");

            entity.ToTable("Employee");

            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Employee__Depart__2F10007B");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK__Employee__Positi__2E1BDC42");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Event__3214EC0765ACBDEF");

            entity.ToTable("Event");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Module).WithMany(p => p.Events)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK__Event__ModuleId__36B12243");

            entity.HasOne(d => d.Type).WithMany(p => p.Events)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Event__TypeId__37A5467C");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EventTyp__3214EC073B94A5E1");

            entity.ToTable("EventType");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<MapModule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MapModul__3214EC07FCD6E7E0");

            entity.ToTable("MapModule");

            entity.HasOne(d => d.Map).WithMany(p => p.MapModules)
                .HasForeignKey(d => d.MapId)
                .HasConstraintName("FK__MapModule__MapId__46E78A0C");

            entity.HasOne(d => d.Module).WithMany(p => p.MapModules)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK__MapModule__Modul__47DBAE45");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Material__3214EC07DD620CA4");

            entity.ToTable("Material");

            entity.Property(e => e.FileName).HasMaxLength(100);
            entity.Property(e => e.MaterialName).HasMaxLength(100);

            entity.HasOne(d => d.Module).WithMany(p => p.Materials)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK__Material__Module__412EB0B6");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Module__3214EC073A3A5AE6");

            entity.ToTable("Module");

            entity.Property(e => e.CodeName).HasMaxLength(100);
            entity.Property(e => e.DateCreate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.LastChangeBy).WithMany(p => p.ModuleLastChangeBies)
                .HasForeignKey(d => d.LastChangeById)
                .HasConstraintName("FK__Module__LastChan__31EC6D26");

            entity.HasOne(d => d.Previous).WithMany(p => p.InversePrevious)
                .HasForeignKey(d => d.PreviousId)
                .HasConstraintName("FK__Module__Previous__32E0915F");

            entity.HasOne(d => d.ResponsibleEmployee).WithMany(p => p.ModuleResponsibleEmployees)
                .HasForeignKey(d => d.ResponsibleEmployeeId)
                .HasConstraintName("FK__Module__Responsi__33D4B598");

            entity.HasOne(d => d.Status).WithMany(p => p.Modules)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("Module___fk___Status");
        });

        modelBuilder.Entity<ModuleAccess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ModuleAc__3214EC0747D53245");

            entity.ToTable("ModuleAccess");

            entity.HasOne(d => d.Module).WithMany(p => p.ModuleAccesses)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK__ModuleAcc__Modul__5629CD9C");

            entity.HasOne(d => d.Position).WithMany(p => p.ModuleAccesses)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK__ModuleAcc__Posit__571DF1D5");
        });

        modelBuilder.Entity<ModuleStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ModuleSt__3214EC07A22CA773");

            entity.ToTable("ModuleStatus");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<ModuleWork>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ModuleWo__3214EC078F6BB505");

            entity.ToTable("ModuleWork");

            entity.HasOne(d => d.Employee).WithMany(p => p.ModuleWorks)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__ModuleWor__Emplo__5070F446");

            entity.HasOne(d => d.Module).WithMany(p => p.ModuleWorks)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("ModuleWork___fk___Module");

            entity.HasOne(d => d.Role).WithMany(p => p.ModuleWorks)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__ModuleWor__RoleI__5165187F");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Position__3214EC0786A1DB3B");

            entity.ToTable("Position");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC0787AB7880");

            entity.ToTable("Question");

            entity.Property(e => e.Answer).HasMaxLength(100);
            entity.Property(e => e.Text).HasMaxLength(300);

            entity.HasOne(d => d.Testing).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TestingId)
                .HasConstraintName("FK__Question__Testin__3E52440B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC07689D5997");

            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Testing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Testing__3214EC07713F25F3");

            entity.ToTable("Testing");

            entity.HasOne(d => d.Module).WithMany(p => p.Testings)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK__Testing__ModuleI__3B75D760");

            entity.HasOne(d => d.Type).WithMany(p => p.Testings)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Testing__TypeId__3A81B327");
        });

        modelBuilder.Entity<TestingResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TestingR__3214EC0781C88BD9");

            entity.ToTable("TestingResult");

            entity.HasOne(d => d.Map).WithMany(p => p.TestingResults)
                .HasForeignKey(d => d.MapId)
                .HasConstraintName("FK__TestingRe__Map__4AB81AF0");

            entity.HasOne(d => d.Testing).WithMany(p => p.TestingResults)
                .HasForeignKey(d => d.TestingId)
                .HasConstraintName("FK__TestingRe__Testi__4BAC3F29");
        });

        modelBuilder.Entity<TestingType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TestingT__3214EC07E156E77A");

            entity.ToTable("TestingType");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
