using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            modelBuilder.Entity<SPMaterialIssuance>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPItemList>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPTotalCount>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPAssignedVillageToVI>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpGetIndent>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpUserMenu>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpUserChildMenu>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPIndentMaster>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPPoList>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpPoByPoId>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPMaterialReceivedCorrespondenceToPo>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPItemMaster>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPBomDetail>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPIndentMasterForWarehouseIncharge>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpGrnMaster>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpHoClosure>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpUpdateQty>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpWarehouseStocklog>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPCreateIndent>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpIndentMasterForSiteEngineer>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPStockBook>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpGetIndentChallan>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpSearchWarehouseStock>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpUserList>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpIndentPendingReport>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpItemBasedOnBom>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpReportByAll>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPValidateByHo>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpInwardReport>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpOutwardReport>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpFreezedReport>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpReturnedFreshStock>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPAssignedJobWorkToWareHouse>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPWareHouseStock>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPStockByBom>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPStockReservation>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpWareHouseTransferPendingListForHo>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpWarehouseTransferChildForHo>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpWareHouseTransferPendingListForWareHouse>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPPoSearch>().HasNoKey().ToView(null);
            modelBuilder.Entity<SpWareHouseStockReport>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPPoDetail>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPPoMasterDetail>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPWareHouseVillageIncharge>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPWareHouseDamgedStock>().HasNoKey().ToView(null);
            modelBuilder.Entity<SPReportOnReceiving>().HasNoKey().ToView(null);
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
        public virtual DbSet<VillageIncharges> TblVillageInchargeForWareHouse { get; set; }
        public virtual DbSet<WarehouseIncharges> TblWarehouseIncharge { get; set; }
        public virtual DbSet<JobWorkcategories> TblJobWorkCategory { get; set; }
        public virtual DbSet<JobWork> TblJobWork { get; set; }
        public virtual DbSet<ItemToWarehouse> TblItemToWarehouse { get; set; }
        public virtual DbSet<Bom> TblBom { get; set; }
        public virtual DbSet<Suppliers> TblSupplier { get; set; }
        public virtual DbSet<MaterialIssuance> TblMaterialIssuance { get; set; }
        public virtual DbSet<Pochild> TblPoChild { get; set; }
        public virtual DbSet<PoMaster> TblPoMaster { get; set; }
        public virtual DbSet<JobDescription> TblJobDescription { get; set; }
        public virtual DbSet<User> TblUser { get; set; }
        public virtual DbSet<VillageWithVillageIncharge> TblVillageWithVillageIncharge { get; set; }        
        public virtual DbSet<Menu> TblMenuList { get; set; }
        public virtual DbSet<UserMenu> TblUserMenu { get; set; }
        public virtual DbSet<IndentMaster> TblIndentMaster { get; set; }
        public virtual DbSet<InwardDocuments> TblInwardDocuments { get; set; }
        public virtual DbSet<MaterialReceivedbyPo> TblMaterialReceivedbyPo { get; set; }
        public virtual DbSet<Indent> TblIndent { get; set; }
        public virtual DbSet<Challan> TblChallan { get; set; }
        public virtual DbSet<PoVehicleDetail> TblPoVehicleDetail { get; set; }
        public virtual DbSet<Closure> TblClosure { get; set; }
        public virtual DbSet<IndentChallan> TblIndentChallan { get; set; }
        public virtual DbSet<StockPassbook> TblStockPassbook { get; set; }
        public virtual DbSet<WarehouseTransfer> TblWarehouseTransfer { get; set; }
        public virtual DbSet<WarehouseTransferChild> TblWarehouseTransferChild { get; set; }
     




        #region StoredProcedure

        public virtual DbSet<SPBlockByDistrict> SPBlockByDistrict { get; set; }
        public virtual DbSet<SPGramPanchyatByBlock> SPGramPanchyatByBlock { get; set; }
        public virtual DbSet<SPVillageList> SPVillageList { get; set; }
        public virtual DbSet<SPVillageIncharge> SPVillageIncharge { get; set; }
        public virtual DbSet<SPWarehouseList> SPWarehouseList { get; set; }
        public virtual DbSet<SPJobWorkList> SPJobWorkList { get; set; }
        public virtual DbSet<SPItemInWareHouse> SPItemInWareHouse { get; set; }        
        public virtual DbSet<SPMaterialIssuance> SPMaterialIssuance { get; set; }
        public virtual DbSet<SPItemList> SPItemList { get; set; }
        public virtual DbSet<SPTotalCount> SPTotalCount { get; set; }
        public virtual DbSet<SPAssignedVillageToVI> SPAssignedVillageToVI { get; set; }
        public virtual DbSet<SpGetIndent> SpGetIndent { get; set; }
        public virtual DbSet<SpUserMenu> SpUserMenu { get; set; }
        public virtual DbSet<SpUserChildMenu> SpUserChildMenu { get; set; }
        public virtual DbSet<SPIndentMaster> SPIndentMaster { get; set; }
        public virtual DbSet<SPPoList> SPPoList { get; set; }
        public virtual DbSet<SpPoByPoId> SpPoByPoId { get; set; }
        public virtual DbSet<SPMaterialReceivedCorrespondenceToPo> SPMaterialReceivedCorrespondenceToPo { get; set; }
        public virtual DbSet<SPItemMaster> SPItemMaster { get; set; }
        public virtual DbSet<SPBomDetail> SPBomDetail { get; set; }
        public virtual DbSet<SPIndentMasterForWarehouseIncharge> SPIndentMasterForWarehouseIncharge { get; set; }
        public virtual DbSet<SpGrnMaster> SpGrnMaster { get; set; }
        public virtual DbSet<SpHoClosure> SpHoClosure { get; set; }
        public virtual DbSet<SpUpdateQty> SpUpdateQty { get; set; }
        public virtual DbSet<SpWarehouseStocklog> SpWarehouseStocklog { get; set; }
        public virtual DbSet<SPCreateIndent> SPCreateIndent { get; set; }
        public virtual DbSet<SpIndentMasterForSiteEngineer> SpIndentMasterForSiteEngineer { get; set; }
        public virtual DbSet<SPStockBook> SPStockBook { get; set; }
        public virtual DbSet<SpGetIndentChallan> SpGetIndentChallan { get; set; }
        public virtual DbSet<SpSearchWarehouseStock> SpSearchWarehouseStock { get; set; }
        public virtual DbSet<SpUserList> SpUserList { get; set; }
        public virtual DbSet<SpIndentPendingReport> SpIndentPendingReport { get; set; }
        public virtual DbSet<SpItemBasedOnBom> SpItemBasedOnBom { get; set; }
        public virtual DbSet<SpReportByAll> SpReportByAll { get; set; }
        public virtual DbSet<SPValidateByHo> SPValidateByHo { get; set; }
        public virtual DbSet<SpInwardReport> SpInwardReport { get; set; }
        public virtual DbSet<SpOutwardReport> SpOutwardReport { get; set; }
        public virtual DbSet<SpFreezedReport> SpFreezedReport { get; set; }
        public virtual DbSet<SpReturnedFreshStock> SpReturnedFreshStock { get; set; }
        public virtual DbSet<SPAssignedJobWorkToWareHouse> SPAssignedJobWorkToWareHouse { get; set; }
        public virtual DbSet<SPWareHouseStock> SPWareHouseStock { get; set; }
        public virtual DbSet<SPStockByBom> SPStockByBom { get; set; }
        public virtual DbSet<SPStockReservation> SPStockReservation { get; set; }
        public virtual DbSet<SpWareHouseTransferPendingListForHo> SpWareHouseTransferPendingListForHo { get; set; }
        public virtual DbSet<SpWarehouseTransferChildForHo> SpWarehouseTransferChildForHo { get; set; }
        public virtual DbSet<SpWareHouseTransferPendingListForWareHouse> SpWareHouseTransferPendingListForWareHouse { get; set; }
        public virtual DbSet<SPPoSearch> SPPoSearch { get; set; }
        public virtual DbSet<SpWareHouseStockReport> SpWareHouseStockReport { get; set; }
        public virtual DbSet<SPPoDetail> SPPoDetail { get; set; }
        public virtual DbSet<SPPoMasterDetail> SPPoMasterDetail { get; set; }
        public virtual DbSet<SPWareHouseVillageIncharge> SPWareHouseVillageIncharge { get; set; }
        public virtual DbSet<SPWareHouseDamgedStock> SPWareHouseDamgedStock { get; set; }
        public virtual DbSet<SPReportOnReceiving> SPReportOnReceiving { get; set; }

        #endregion










    }

}
