﻿// <auto-generated />
using System;
using EAS_Hub.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EAS_Hub.Migrations
{
    [DbContext(typeof(EasFullDbContext))]
    [Migration("20241126182838_AddColumn")]
    partial class AddColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EAS_Hub.DbModels.AdaptationMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateCreate")
                        .HasColumnType("datetime");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<bool?>("Hired")
                        .HasColumnType("bit");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Adaptati__3214EC070D68EC25");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("PositionId");

                    b.ToTable("AdaptationMap", (string)null);
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__Departme__3214EC079133C078");

                    b.ToTable("Department", (string)null);
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Jwt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Login")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Employee__3214EC073EC684FD");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("PositionId");

                    b.ToTable("Employee", (string)null);
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("ModuleId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Event__3214EC0765ACBDEF");

                    b.HasIndex("ModuleId");

                    b.HasIndex("TypeId");

                    b.ToTable("Event", (string)null);
                });

            modelBuilder.Entity("EAS_Hub.DbModels.EventType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__EventTyp__3214EC073B94A5E1");

                    b.ToTable("EventType", (string)null);
                });

            modelBuilder.Entity("EAS_Hub.DbModels.MapModule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<int?>("MapId")
                        .HasColumnType("int");

                    b.Property<int?>("ModuleId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__MapModul__3214EC07FCD6E7E0");

                    b.HasIndex("MapId");

                    b.HasIndex("ModuleId");

                    b.ToTable("MapModule", (string)null);
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Data")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MaterialName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("ModuleId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PK__Material__3214EC07DD620CA4");

                    b.HasIndex("ModuleId");

                    b.ToTable("Material", (string)null);
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodeName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("CompleteDeadline")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCreate")
                        .HasColumnType("datetime");

                    b.Property<int?>("DevelopDeadline")
                        .HasColumnType("int");

                    b.Property<int?>("LastChangeById")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("PreviousId")
                        .HasColumnType("int");

                    b.Property<int?>("ResponsibleEmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Module__3214EC073A3A5AE6");

                    b.HasIndex("LastChangeById");

                    b.HasIndex("PreviousId");

                    b.HasIndex("ResponsibleEmployeeId");

                    b.HasIndex("StatusId");

                    b.ToTable("Module", (string)null);
                });

            modelBuilder.Entity("EAS_Hub.DbModels.ModuleAccess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ModuleId")
                        .HasColumnType("int");

                    b.Property<int?>("PositionId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__ModuleAc__3214EC0747D53245");

                    b.HasIndex("ModuleId");

                    b.HasIndex("PositionId");

                    b.ToTable("ModuleAccess", (string)null);
                });

            modelBuilder.Entity("EAS_Hub.DbModels.ModuleStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__ModuleSt__3214EC07A22CA773");

                    b.ToTable("ModuleStatus", (string)null);
                });

            modelBuilder.Entity("EAS_Hub.DbModels.ModuleWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Accepted")
                        .HasColumnType("bit");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<bool?>("Main")
                        .HasColumnType("bit");

                    b.Property<int?>("ModuleId")
                        .HasColumnType("int");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__ModuleWo__3214EC078F6BB505");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ModuleId");

                    b.HasIndex("RoleId");

                    b.ToTable("ModuleWork", (string)null);
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__Position__3214EC0786A1DB3B");

                    b.ToTable("Position", (string)null);
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("TestingId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id")
                        .HasName("PK__Question__3214EC0787AB7880");

                    b.HasIndex("TestingId");

                    b.ToTable("Question", (string)null);
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__Role__3214EC07689D5997");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Testing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ModuleId")
                        .HasColumnType("int");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Testing__3214EC07713F25F3");

                    b.HasIndex("ModuleId");

                    b.HasIndex("TypeId");

                    b.ToTable("Testing", (string)null);
                });

            modelBuilder.Entity("EAS_Hub.DbModels.TestingResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Correct")
                        .HasColumnType("int");

                    b.Property<int?>("Error")
                        .HasColumnType("int");

                    b.Property<int?>("MapId")
                        .HasColumnType("int");

                    b.Property<int?>("TestingId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__TestingR__3214EC0781C88BD9");

                    b.HasIndex("MapId");

                    b.HasIndex("TestingId");

                    b.ToTable("TestingResult", (string)null);
                });

            modelBuilder.Entity("EAS_Hub.DbModels.TestingType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__TestingT__3214EC07E156E77A");

                    b.ToTable("TestingType", (string)null);
                });

            modelBuilder.Entity("EAS_Hub.DbModels.AdaptationMap", b =>
                {
                    b.HasOne("EAS_Hub.DbModels.Department", "Department")
                        .WithMany("AdaptationMaps")
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("AdaptationMap___fk_Department");

                    b.HasOne("EAS_Hub.DbModels.Employee", "Employee")
                        .WithMany("AdaptationMaps")
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("FK__Adaptatio__Emplo__440B1D61");

                    b.HasOne("EAS_Hub.DbModels.Position", "Position")
                        .WithMany("AdaptationMaps")
                        .HasForeignKey("PositionId")
                        .HasConstraintName("AdaptationMap___fk_Position");

                    b.Navigation("Department");

                    b.Navigation("Employee");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Employee", b =>
                {
                    b.HasOne("EAS_Hub.DbModels.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("FK__Employee__Depart__2F10007B");

                    b.HasOne("EAS_Hub.DbModels.Position", "Position")
                        .WithMany("Employees")
                        .HasForeignKey("PositionId")
                        .HasConstraintName("FK__Employee__Positi__2E1BDC42");

                    b.Navigation("Department");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Event", b =>
                {
                    b.HasOne("EAS_Hub.DbModels.Module", "Module")
                        .WithMany("Events")
                        .HasForeignKey("ModuleId")
                        .HasConstraintName("FK__Event__ModuleId__36B12243");

                    b.HasOne("EAS_Hub.DbModels.EventType", "Type")
                        .WithMany("Events")
                        .HasForeignKey("TypeId")
                        .HasConstraintName("FK__Event__TypeId__37A5467C");

                    b.Navigation("Module");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.MapModule", b =>
                {
                    b.HasOne("EAS_Hub.DbModels.AdaptationMap", "Map")
                        .WithMany("MapModules")
                        .HasForeignKey("MapId")
                        .HasConstraintName("FK__MapModule__MapId__46E78A0C");

                    b.HasOne("EAS_Hub.DbModels.Module", "Module")
                        .WithMany("MapModules")
                        .HasForeignKey("ModuleId")
                        .HasConstraintName("FK__MapModule__Modul__47DBAE45");

                    b.Navigation("Map");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Material", b =>
                {
                    b.HasOne("EAS_Hub.DbModels.Module", "Module")
                        .WithMany("Materials")
                        .HasForeignKey("ModuleId")
                        .HasConstraintName("FK__Material__Module__412EB0B6");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Module", b =>
                {
                    b.HasOne("EAS_Hub.DbModels.Employee", "LastChangeBy")
                        .WithMany("ModuleLastChangeBies")
                        .HasForeignKey("LastChangeById")
                        .HasConstraintName("FK__Module__LastChan__31EC6D26");

                    b.HasOne("EAS_Hub.DbModels.Module", "Previous")
                        .WithMany("InversePrevious")
                        .HasForeignKey("PreviousId")
                        .HasConstraintName("FK__Module__Previous__32E0915F");

                    b.HasOne("EAS_Hub.DbModels.Employee", "ResponsibleEmployee")
                        .WithMany("ModuleResponsibleEmployees")
                        .HasForeignKey("ResponsibleEmployeeId")
                        .HasConstraintName("FK__Module__Responsi__33D4B598");

                    b.HasOne("EAS_Hub.DbModels.ModuleStatus", "Status")
                        .WithMany("Modules")
                        .HasForeignKey("StatusId")
                        .HasConstraintName("Module___fk___Status");

                    b.Navigation("LastChangeBy");

                    b.Navigation("Previous");

                    b.Navigation("ResponsibleEmployee");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.ModuleAccess", b =>
                {
                    b.HasOne("EAS_Hub.DbModels.Module", "Module")
                        .WithMany("ModuleAccesses")
                        .HasForeignKey("ModuleId")
                        .HasConstraintName("FK__ModuleAcc__Modul__5629CD9C");

                    b.HasOne("EAS_Hub.DbModels.Position", "Position")
                        .WithMany("ModuleAccesses")
                        .HasForeignKey("PositionId")
                        .HasConstraintName("FK__ModuleAcc__Posit__571DF1D5");

                    b.Navigation("Module");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.ModuleWork", b =>
                {
                    b.HasOne("EAS_Hub.DbModels.Employee", "Employee")
                        .WithMany("ModuleWorks")
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("FK__ModuleWor__Emplo__5070F446");

                    b.HasOne("EAS_Hub.DbModels.Module", "Module")
                        .WithMany("ModuleWorks")
                        .HasForeignKey("ModuleId")
                        .HasConstraintName("ModuleWork___fk___Module");

                    b.HasOne("EAS_Hub.DbModels.Role", "Role")
                        .WithMany("ModuleWorks")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__ModuleWor__RoleI__5165187F");

                    b.Navigation("Employee");

                    b.Navigation("Module");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Question", b =>
                {
                    b.HasOne("EAS_Hub.DbModels.Testing", "Testing")
                        .WithMany("Questions")
                        .HasForeignKey("TestingId")
                        .HasConstraintName("FK__Question__Testin__3E52440B");

                    b.Navigation("Testing");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Testing", b =>
                {
                    b.HasOne("EAS_Hub.DbModels.Module", "Module")
                        .WithMany("Testings")
                        .HasForeignKey("ModuleId")
                        .HasConstraintName("FK__Testing__ModuleI__3B75D760");

                    b.HasOne("EAS_Hub.DbModels.TestingType", "Type")
                        .WithMany("Testings")
                        .HasForeignKey("TypeId")
                        .HasConstraintName("FK__Testing__TypeId__3A81B327");

                    b.Navigation("Module");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.TestingResult", b =>
                {
                    b.HasOne("EAS_Hub.DbModels.AdaptationMap", "Map")
                        .WithMany("TestingResults")
                        .HasForeignKey("MapId")
                        .HasConstraintName("FK__TestingRe__Map__4AB81AF0");

                    b.HasOne("EAS_Hub.DbModels.Testing", "Testing")
                        .WithMany("TestingResults")
                        .HasForeignKey("TestingId")
                        .HasConstraintName("FK__TestingRe__Testi__4BAC3F29");

                    b.Navigation("Map");

                    b.Navigation("Testing");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.AdaptationMap", b =>
                {
                    b.Navigation("MapModules");

                    b.Navigation("TestingResults");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Department", b =>
                {
                    b.Navigation("AdaptationMaps");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Employee", b =>
                {
                    b.Navigation("AdaptationMaps");

                    b.Navigation("ModuleLastChangeBies");

                    b.Navigation("ModuleResponsibleEmployees");

                    b.Navigation("ModuleWorks");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.EventType", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Module", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("InversePrevious");

                    b.Navigation("MapModules");

                    b.Navigation("Materials");

                    b.Navigation("ModuleAccesses");

                    b.Navigation("ModuleWorks");

                    b.Navigation("Testings");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.ModuleStatus", b =>
                {
                    b.Navigation("Modules");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Position", b =>
                {
                    b.Navigation("AdaptationMaps");

                    b.Navigation("Employees");

                    b.Navigation("ModuleAccesses");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Role", b =>
                {
                    b.Navigation("ModuleWorks");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.Testing", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("TestingResults");
                });

            modelBuilder.Entity("EAS_Hub.DbModels.TestingType", b =>
                {
                    b.Navigation("Testings");
                });
#pragma warning restore 612, 618
        }
    }
}
