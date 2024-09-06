using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using BankMegaTechTest.Models.Entities;

namespace BankMegaTechTest.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<TrBpkb> TrBpkbs { get; set; }
    public DbSet<MsStorageLocation> MsStorageLocations { get; set; }
    public DbSet<MsUser> MsUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}


