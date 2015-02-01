using HospiceNiagara.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace HospiceNiagara.DAL
{
    public class HospiceCFEntities : DbContext
    {
        public DbSet<FileSortType> FileSortTypes { get; set; }
        public DbSet<FileStorage> FileStorages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}