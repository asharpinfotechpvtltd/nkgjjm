using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nkgjjm.Models;
using Nkgjjm.StoredProcedure;
using IdentityModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Nkgjjm.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public void DetachAllEntities()
        {
            var changedEntriesCopy = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<SPBlockByDistrict>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPGramPanchyatByBlock>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPVillageList>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPVillageIncharge>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPWarehouseList>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPJobWorkList>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPItemInWareHouse>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPBomList>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPMaterialIssuance>().HasNoKey().ToView(null);

            


        }

        public virtual DbSet<Contractors> TblContractor { get; set; }
        public virtual DbSet<Designation> TblDesignation { get; set; }
        public virtual DbSet<Employee> TblEmployee { get; set; }
        public virtual DbSet<ItemMaster> TblItemMaster { get; set; }
        public virtual DbSet<Units> TblUnits { get; set; }
        public virtual DbSet<Warehouse> TblWarehouse { get; set; }        
        public virtual DbSet<DistBlock> TblBlock { get; set; }
        public virtual DbSet<GramPanchayats> TblGramPanchayat { get; set; }
        public virtual DbSet<Villages> TblVillageCode { get; set; }
        public virtual DbSet<Districts> TblDistrict { get; set; }
        public virtual DbSet<VillageIncharges> TblVillageIncharge { get; set; }
        public virtual DbSet<WarehouseIncharges> TblWarehouseIncharge { get; set; }
        public virtual DbSet<JobWorkcategories> TblJobWorkCategory { get; set; }
        public virtual DbSet<JobWork> TblJobWork { get; set; }
        public virtual DbSet<ItemToWarehouse> TblItemToWarehouse { get; set; }
        public virtual DbSet<Bom> TblBom { get; set; }
        public virtual DbSet<Suppliers> TblSupplier { get; set; }




        #region StoredProcedure

        public virtual DbSet<SPBlockByDistrict> SPBlockByDistrict { get; set; }
        public virtual DbSet<SPGramPanchyatByBlock> SPGramPanchyatByBlock { get; set; }
        public virtual DbSet<SPVillageList> SPVillageList { get; set; }
        public virtual DbSet<SPVillageIncharge> SPVillageIncharge { get; set; }
        public virtual DbSet<SPWarehouseList> SPWarehouseList { get; set; }
        public virtual DbSet<SPJobWorkList> SPJobWorkList { get; set; }
        public virtual DbSet<SPItemInWareHouse> SPItemInWareHouse { get; set; }
        public virtual DbSet<SPBomList> SPBomList { get; set; }
        public virtual DbSet<SPMaterialIssuance> SPMaterialIssuance { get; set; }
        public DbSet<Nkgjjm.Models.MaterialIssuance>? MaterialIssuance { get; set; }
        #endregion










    }

}
