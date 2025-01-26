using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Healper
{
    public static class SoftDeleteHelper
    {
        public static void ApplySoftDelete<T>(T entity, string deletedBy) where T : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            // Use reflection to set common soft delete properties
            var type = entity.GetType();
            type.GetProperty("IsDeleted")?.SetValue(entity, true);
            type.GetProperty("DeletedBy")?.SetValue(entity, deletedBy);
            type.GetProperty("DeletedAt")?.SetValue(entity, DateTime.Now);
        }
    }

}
