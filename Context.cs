using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace WPFMVVM
{
    public class Context: DbContext
    {
        public Context() : base("ConnectionTodb")
        {
            
        }
        public DbSet<Model> People { get; set; }
    }
}
