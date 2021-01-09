using GOTurystyka.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOTurystyka.Contexts
{
    public class GOTurystykaDBContext: DbContext
    {
        public DbSet<Foreman> Foremen { get; set; }
        //public DbSet<> MyProperty { get; set; }
    }
}
