namespace CNW_WebBanQuanAo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TTSANPHAM")]
    public partial class TTSANPHAM
    {
        public int? MaMH { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaQA { get; set; }

        [StringLength(10)]
        public string MaSize { get; set; }

        [StringLength(20)]
        public string MaMau { get; set; }

        [StringLength(50)]
        public string TenMH { get; set; }

        public string UrlAnh { get; set; }

        public string KieuDang { get; set; }

        public string ChatLieu { get; set; }

        public int? MaNSX { get; set; }

        public int? GiaBan { get; set; }

        public string TenNSX { get; set; }
    }
}
