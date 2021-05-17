using Microsoft.Extensions.Configuration;
using ShopBridge.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Service
{
	public class BaseService : IBaseService
	{
		private readonly IConfiguration Configuration;
		public BaseService(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		public string ApiUrl => Configuration.GetSection("APIUrl").Value;
	}
}
