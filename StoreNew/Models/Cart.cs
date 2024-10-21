using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KAMStoreNew.Models
{
    public class Cart
    {
        [Key]
        public long Cart_ID { get; set; }
        public string Cart_Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public Nullable<decimal> Cart_Price { get; set; }
        public string Cart_Img { get; set; }
        public long CartGroup_ID { get; set; }
        public long Quantity { get; set; }
        public long OrderId { get; set; }
        public string UserID { get; set; }
    }
}