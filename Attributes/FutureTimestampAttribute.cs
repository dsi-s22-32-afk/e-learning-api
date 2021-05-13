using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UniWall.Utilities;

namespace UniWall.Attributes
{
    public class FutureTimestampAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            long now = TimeUtil.GetMiliseconds(DateTime.Now);
            long? target = value as long?;

            return target != null && target > now;
        }
    }
}
