using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApi.Models
{
    public class SalesContext:DbContext
    {
        public SalesContext(DbContextOptions<SalesContext>options) : base (options)
        {

        }

    }
}
