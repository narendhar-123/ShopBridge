using ShopBridge.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopBridge.Service.Mapper
{
    public static class BaseMapper
    {
        public static BaseEntity BaseMap(this BaseEntity entity)
        {
            entity.CreatedBy = 1;
            entity.CreatedOn = DateTime.Now;
            entity.ModifiedBy = 1;
            entity.ModifiedOn = DateTime.Now;
            return entity;
        }
    }
}
