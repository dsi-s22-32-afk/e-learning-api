using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Attributes
{
    public class UserRoleAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return true;
        }
    }
}
