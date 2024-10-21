using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KAMStoreNew.Models
{
    public class Order
    {
        [Key]
        public long Id { get; set; }
        public long OrderId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime OrderDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Total { get; set; }
        public long UserId { get; set; }
        public virtual InformationUser InformationUser { get; set; }
    }
}