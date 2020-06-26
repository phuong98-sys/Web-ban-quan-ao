namespace CNW_WebBanQuanAo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SANPHAM")]
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            GIAODICH = new HashSet<GIAODICH>();
            GIOHANG = new HashSet<GIOHANG>();
        }

        [Key]
        public int MaQA { get; set; }

        public int? MaMH { get; set; }

        [StringLength(10)]
        public string MaSize { get; set; }

        [StringLength(50)]
        public string MaMau { get; set; }

        public int? SoLuong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GIAODICH> GIAODICH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GIOHANG> GIOHANG { get; set; }

        public virtual MATHANG MATHANG { get; set; }

        public virtual MAU MAU { get; set; }

        public virtual SIZE SIZE { get; set; }
    }
}
