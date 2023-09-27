namespace KampusSggwBackend.Data;

using KampusSggwBackend.Domain.JwtToken;
using KampusSggwBackend.Domain.Schedule;
using KampusSggwBackend.Domain.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

public class DataContext : IdentityDbContext<UserAccount, UserRole, Guid>
{
    // Tables
    public DbSet<RevokedToken> RevokedTokens { get; set; }
    public DbSet<VerificationCode> VerificationCodes { get; set; }
    public DbSet<PasswordResetRequest> PasswordResetRequests { get; set; }
    public DbSet<UserEmailChange> UserEmailChanges { get; set; }

    // Schedule
    public DbSet<Lecturer> Lecturers { get; set; }
    public DbSet<ScheduleClass> Classes { get; set; }
    public DbSet<Lesson> Lessons { get; set; }

    // Constructor
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    // Methods
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}
