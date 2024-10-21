using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KAMStoreNew.Models
{
    public class Brand
    {
        [Key]
        public long BrandID { get; set; }
        public string BrandName { get; set; }
        public Nullable<long> CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}