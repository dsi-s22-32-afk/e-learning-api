using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UniWall.Data.Contexts;

namespace UniWall.Attributes
{
    public class ExistsInDatabaseAttribute : ValidationAttribute
    {
        private readonly Type _entityType;

        public ExistsInDatabaseAttribute(Type entityType)
        {
            _entityType = entityType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            ApiDbContext db = (ApiDbContext)context.GetService(typeof(ApiDbContext));

            bool isValid = value is IEnumerable ? TestForArray(value as IEnumerable, db) : TestForSingleValue(value, db); 

            return isValid ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }

        private bool TestForSingleValue(object value, ApiDbContext db)
        {
            return db.Find(_entityType, value) != null;
        }

        private bool TestForArray(IEnumerable value, ApiDbContext db)
        {
            bool result = true;
            foreach(var item in value)
            {
                if(db.Find(_entityType, item) == null)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}
