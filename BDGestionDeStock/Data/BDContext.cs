using BDGestionDeStock.Data.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDGestionDeStock.Data
{
    public class BDContext1 : DbContext 
    {
        public DbSet<Producto> Productos { get; set; } //
        public BDContext1(DbContextOptions options) : base(options)
        {

        }
    }
}
