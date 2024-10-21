using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KAMStoreNew.Models
{
    public class Product
    {
        [Key]
        public long ProductID { get; set; }
        [Required(ErrorMessage = "Không được để trống!!!")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Không được để trống!!!")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public Nullable<decimal> Price { get; set; }
        [Required(ErrorMessage = "Không được để trống!!!")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [Required(ErrorMessage = "Không được để trống!!!")]
        public string AvailabilityStatus { get; set; }
        public string Img { get; set; }
        [Required(ErrorMessage = "Không được để trống!!!")]
        public Nullable<long> CategoryID { get; set; }
        [Required(ErrorMessage = "Không được để trống!!!")]
        public Nullable<long> BrandID { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
    }
}