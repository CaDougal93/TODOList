using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TODOList.Models;

namespace TODOList.DAL
{
    public class TODOItemContext : DbContext
    {
        public TODOItemContext() : base("TODO"){ }
        public DbSet<TODOItem> TODOItems { get; set; }
    }
}