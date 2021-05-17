using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Domain.Model
{
    public class BaseEntity
    {
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
