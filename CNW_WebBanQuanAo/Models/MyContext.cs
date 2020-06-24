namespace CNW_WebBanQuanAo.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyContext : DbContext
    {
        public MyContext()
            : base("name=MyContext")
        {
        }

        public virtual DbSet<ANH> ANH { get; set; }
        public virtual DbSet<GIAODICH> GIAODICH { get; set; }
        public virtual DbSet<GIOHANG> GIOHANG { get; set; }
        public virtual DbSet<HOADON> HOADON { get; set; }
        public virtual DbSet<MATHANG> MATHANG { get; set; }
        public virtual DbSet<MAU> MAU { get; set; }
        public virtual DbSet<NHASANXUAT> NHASANXUAT { get; set; }
        public virtual DbSet<PHANHOI> PHANHOI { get; set; }
        public virtual DbSet<SANPHAM> SANPHAM { get; set; }
        public virtual DbSet<SIZE> SIZE { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TAIKHOAN> TAIKHOAN { get; set; }
        public virtual DbSet<TTSANPHAM> TTSANPHAM { get; set; }
      //  public virtual DbSet<CTHOADON> CTHOADON { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ANH>()
                .Property(e => e.UrlAnh)
                .IsUnicode(false);

            modelBuilder.Entity<GIOHANG>()
                .Property(e => e.MaKH)
                .IsFixedLength();

            modelBuilder.Entity<HOADON>()
                .Property(e => e.MaKH)
                .IsFixedLength();

            modelBuilder.Entity<HOADON>()
                .Property(e => e.NguoiChot)
                .IsFixedLength();

            modelBuilder.Entity<HOADON>()
                .HasMany(e => e.GIAODICH)
                .WithRequired(e => e.HOADON)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MATHANG>()
                .HasMany(e => e.ANH)
                .WithRequired(e => e.MATHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NHASANXUAT>()
                .HasMany(e => e.MATHANG)
                .WithOptional(e => e.NHASANXUAT)
                .HasForeignKey(e => e.MaNSX);

            modelBuilder.Entity<PHANHOI>()
                .Property(e => e.MaKH)
                .IsFixedLength();

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.MaSize)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.GIAODICH)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.GIOHANG)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SIZE>()
                .Property(e => e.MaSize)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.Username)
                .IsFixedLength();

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.SDT)
                .IsFixedLength();

            modelBuilder.Entity<TAIKHOAN>()
                .HasMany(e => e.GIOHANG)
                .WithRequired(e => e.TAIKHOAN)
                .HasForeignKey(e => e.MaKH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAIKHOAN>()
                .HasMany(e => e.HOADON)
                .WithOptional(e => e.TAIKHOAN)
                .HasForeignKey(e => e.MaKH);

            modelBuilder.Entity<TAIKHOAN>()
                .HasMany(e => e.PHANHOI)
                .WithOptional(e => e.TAIKHOAN)
                .HasForeignKey(e => e.MaKH);

            modelBuilder.Entity<TTSANPHAM>()
                .Property(e => e.MaSize)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TTSANPHAM>()
                .Property(e => e.GiaBan);
        }

        public System.Data.Entity.DbSet<CNW_WebBanQuanAo.Models.LoginModel> LoginModels { get; set; }
    }
}
