using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HCrawler.TvOnlineModels
{
    public partial class TvOnlineContext : DbContext
    {
        public TvOnlineContext()
        {
        }

        public TvOnlineContext(DbContextOptions<TvOnlineContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbChannel> TbChannel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("Server=128.199.183.242;User Id=huyqta;Password=huyqta;Database=tv_online");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<TbChannel>(entity =>
            {
                entity.ToTable("tb_channel", "tv_online");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Channel)
                    .IsRequired()
                    .HasColumnName("channel")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ChannelGroup)
                    .HasColumnName("channel_group")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GetlinkUrl)
                    .IsRequired()
                    .HasColumnName("getlink_url")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.HasStaticStreamUrl)
                    .HasColumnName("has_static_stream_url")
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Language)
                    .HasColumnName("language")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LogoUrl)
                    .HasColumnName("logo_url")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ShortCode)
                    .HasColumnName("short_code")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StreamUrl)
                    .HasColumnName("stream_url")
                    .HasMaxLength(2000)
                    .IsUnicode(false);
            });
        }
    }
}
