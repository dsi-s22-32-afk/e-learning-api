using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniWall.Data.Seeders
{
    interface ISeeder
    {
        public abstract Task Seed();
    }
}
