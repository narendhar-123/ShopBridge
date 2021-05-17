using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Model
{
	public class BaseDTO
	{
		public bool IsDeleted { get; set; }
		public int CreatedById { get; set; }
		public DateTime CreatedOn { get; set; }
		public int? ModifiedById { get; set; }
		public DateTime? ModifiedOn { get; set; }
	}
}
