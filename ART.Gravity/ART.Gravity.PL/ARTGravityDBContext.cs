using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ART.Gravity.PL
{
    public partial class GravityEntities : DbContext
    {
        public GravityEntities()
        {
        }

        public GravityEntities(DbContextOptions<GravityEntities> options)
            : base(options)
        {
        }

        public virtual DbSet<tblGravity> TblGravities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=ART.Gravity.DB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<tblGravity>(entity =>
            {
                entity.ToTable("tblGravity");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ChangeDate).HasColumnType("datetime");
            });

            // Manually add this for the stored proc
            modelBuilder.Entity<spCalcForceResult>().HasNoKey();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
