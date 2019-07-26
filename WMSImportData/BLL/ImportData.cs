using System;
using System.Collections.Generic;
using System.Text;
using static Zh.Common.DbHelper.DapperHelper;
using Dapper;
using System.Data;
using ExcelDataReader;
using WmsReport.Infrastructure.DbCommon;
using Zh.Common.DbHelper;
using Microsoft.Extensions.Options;
using WMSImportData.Config;
using System.Linq;
using System.Data.SqlClient;
using WMSImportData.Model;

namespace WMSImportData.BLL
{
    public class ImportData
    {
        private IWmsDbConnection _wmsDbConnection;
     
        public ImportData(IWmsDbConnection wmsDbConnection)
        {
            _wmsDbConnection = wmsDbConnection;
            
        }
        public int ImportStoTb(string dept,List<StorageinfoTemp> storageinfos)
        {
            int importCount = 0;
            using (var conn = GetOpenConnection(_wmsDbConnection.GetDbConnStr(dept, ""), DbProvider.SqlServer))
            {
                string sqlStr = @"delete from w_StorageInfo_temp";
                conn.Execute(sqlStr);
                foreach (var model in storageinfos)
                {
                    DynamicParameters dp = new DynamicParameters();

                    dp.Add("@CGDHMS", model.CGDHMS);
                    dp.Add("@DeptCode", model.DeptCode);
                    dp.Add("@DmadDept", model.DmadDept);
                    dp.Add("@DmadKS", model.DmadKS);
                    dp.Add("@DmadPhone", model.DmadPhone);
                    dp.Add("@DmadUser", model.DmadUser);
                    dp.Add("@EnComCode", model.EnComCode);
                    dp.Add("@EnComName", model.EnComName);
                    dp.Add("@InOrganize", model.InOrganize);
                    dp.Add("@PackingCode", model.PackingCode);
                    dp.Add("@PactCode", model.PactCode);
                    dp.Add("@PactName", model.PactName);
                    dp.Add("@ProjectCode", model.ProjectCode);
                    dp.Add("@ProjectName", model.ProjectName);
                    dp.Add("@ProjectStatus", model.ProjectStatus);
                    dp.Add("@ProjectUser", model.ProjectUser);
                    dp.Add("@ProSeqno", model.ProSeqno);
                    dp.Add("@PurchaseCode", model.PurchaseCode);
                    dp.Add("@Supplier", model.Supplier);
                    dp.Add("@SupName", model.SupName);
                    dp.Add("@UseDept", model.UseDept);
                    dp.Add("@UseKS", model.UseKS);
                    dp.Add("@UsePhone", model.UsePhone);
                    dp.Add("@UseUser", model.UseUser);
                    dp.Add("@DocEntry", model.DocEntry);
                    dp.Add("@ProductCode", model.ProductCode);
                    dp.Add("@ProductName", model.ProductName);
                    dp.Add("@ProductType", model.ProductType);
                    dp.Add("@ProductUnit", model.ProductUnit);
                    dp.Add("@OrderType", model.OrderType);
                    dp.Add("@StandV", model.StandV);
                    dp.Add("@StandW", model.StandW);
                    dp.Add("@Price", model.Price);
                    dp.Add("@Acount", model.Acount);
                    dp.Add("@PackCode", model.PackCode);
                    dp.Add("@InSDate", model.InSDate);
                    dp.Add("@ZSDate", model.ZSDate);
                    dp.Add("@ProProperty", model.ProProperty);
                    dp.Add("@Accountant", model.Accountant);
                    dp.Add("@Manager", model.Manager);
                    dp.Add("@Remark1", model.Remark1);
                    dp.Add("@Remark2", model.Remark2);
                    dp.Add("@Taizhang", model.Taizhang);
                    dp.Add("@CGDID", model.CGDID);
                    dp.Add("@MISOrderID", model.MISOrderID);
                    dp.Add("@SpcModel", model.SpcModel);
                    dp.Add("@ERPNo", model.ERPNo);
                    dp.Add("@InSUserSign", model.InSUserSign);
                    dp.Add("@PackagingType", model.PackagingType);
                    //dp.Add("@CreateDate", SqlDbType.NVarChar, 50);
                    dp.Add("@ProductCategory", model.ProductCategory);
                    dp.Add("@FirstCate", model.FirstCate);
                    dp.Add("@SecCate", model.SecCate);
                    dp.Add("@Location", model.Location);
                    dp.Add("@PackQty", model.PackQty);
                    dp.Add("@Qty", model.Qty);
                    //dp.Add("@LineNum", SqlDbType.Int, 4);
                    //dp.Add("@ProductCoe", SqlDbType.NVarChar, 100);
                    //dp.Add("@StoStatus", SqlDbType.NVarChar, 50);
                    dp.Add("@WhCode", model.WhCode);
                    dp.Add("@OrderPactCode", model.OrderPactCode);
                    dp.Add("@FacPactCode", model.FacPactCode);
                    //  dp.Add("@id", SqlDbType.Int,4);

                    importCount += conn.Execute("w_StorageInfo_temp_ADD", dp, commandType: CommandType.StoredProcedure);
                }


            }
            return importCount;
        }
        public int ImportMatTb(string dept, List<w_temp_Material> materials)
        {
            int importCount = 0;
            using (var conn = GetOpenConnection(_wmsDbConnection.GetDbConnStr(dept, ""), DbProvider.SqlServer))
            {
                string sqlStr = @"delete from w_temp_Material";
                conn.Execute(sqlStr);
                foreach (var model in materials)
                {
                    DynamicParameters dp = new DynamicParameters();

                    dp.Add("@DocEntry", model.DocEntry);
                    dp.Add("@DeptCode", model.DeptCode);
                    dp.Add("@MaterialName", model.MaterialName);
                    dp.Add("@Supplier", model.Supplier);
                    dp.Add("@SupName", model.SupName);
                    dp.Add("@SpcModel", model.SpcModel);
                    dp.Add("@TypeBig", model.TypeBig);
                    dp.Add("@SKU", model.SKU);
                    dp.Add("@MaterialLevel", model.MaterialLevel);
                    dp.Add("@TypeMid", model.TypeMid);
                    dp.Add("@TypeSmall", model.TypeSmall);
                    dp.Add("@MappingCode", model.MappingCode);
                    dp.Add("@MappingName", model.MappingName);
                    dp.Add("@Price", model.Price);

                    importCount += conn.Execute("w_temp_Material_ADD", dp, commandType: CommandType.StoredProcedure);
                }

            }
            return importCount;
        }
        public int ImportSupplierTb(string dept, List<w_temp_Supplier> materials)
        {
            int importCount = 0;
            using (var conn = GetOpenConnection(_wmsDbConnection.GetDbConnStr(dept, ""), DbProvider.SqlServer))
            {
                string sqlStr = @"delete from w_temp_Supplier";
                conn.Execute(sqlStr);
                foreach (var model in materials)
                {
                    DynamicParameters dp = new DynamicParameters();

                    dp.Add("@DocEntry", model.DocEntry);
                    dp.Add("@DeptCode", model.DeptCode);
                    dp.Add("@SupName", model.SupName);

                    importCount += conn.Execute("w_temp_Supplier_ADD", dp, commandType: CommandType.StoredProcedure);
                }

            }
            return importCount;
        }
        public int ImportStorageTb(string dept, List<w_temp_Storage> _Storages)
        {
            int importCount = 0;
            using (var conn = GetOpenConnection(_wmsDbConnection.GetDbConnStr(dept, ""), DbProvider.SqlServer))
            {
                string sqlStr = @"delete from w_temp_Storage";
                conn.Execute(sqlStr);
                foreach (var model in _Storages)
                {
                    DynamicParameters dp = new DynamicParameters();

                    dp.Add("@DocEntry", model.DocEntry);
                    dp.Add("@DeptCode", model.DeptCode);
                    dp.Add("@StName", model.StName);
                    dp.Add("@SysId", model.SysId);
                    dp.Add("@WhCode", model.WhCode);

                    importCount += conn.Execute("w_temp_Storage_ADD", dp, commandType: CommandType.StoredProcedure);
                }

            }
            return importCount;
        }
        public int ImportProjectTb(string dept, List<w_temp_Project> projects)
        {
            int importCount = 0;
            using (var conn = GetOpenConnection(_wmsDbConnection.GetDbConnStr(dept, ""), DbProvider.SqlServer))
            {
                string sqlStr = @"delete from w_temp_Project";
                conn.Execute(sqlStr);
                foreach (var model in projects)
                {
                    DynamicParameters dp = new DynamicParameters();

                    dp.Add("@DocEntry", model.DocEntry);
                    dp.Add("@DeptCode", model.DeptCode);
                    dp.Add("@PrjName", model.PrjName);

                    importCount += conn.Execute("w_temp_Project_ADD", dp, commandType: CommandType.StoredProcedure);
                }

            }
            return importCount;
        }
        public int ImportTempSheet(string dept,List<w_temp> w_Temps)
        {
            int importCount = 0;
            using (var conn = GetOpenConnection(_wmsDbConnection.GetDbConnStr(dept, ""), DbProvider.SqlServer))
            {
                string sqlStr = @"delete from w_temp";
                conn.Execute(sqlStr);
                foreach (var model in w_Temps)
                {
                    DynamicParameters dp = new DynamicParameters();
                    sqlStr=string.Format("insert into w_temp(ex1,ex2,ex3,ex4) select '{0}','{1}','{2}','{3}'", model.ex1, model.ex2, model.ex3, model.ex4);
                    importCount += conn.Execute(sqlStr);
                    
                }
            }
            return importCount;
        }
        public int ImportTemp2Sheet(string dept, List<w_temp> w_Temps)
        {
            int importCount = 0;
            using (var conn = GetOpenConnection(_wmsDbConnection.GetDbConnStr(dept, ""), DbProvider.SqlServer))
            {
                string sqlStr = @"delete from w_temp2";
                conn.Execute(sqlStr);
                foreach (var model in w_Temps)
                {
                    DynamicParameters dp = new DynamicParameters();
                    sqlStr = string.Format("insert into w_temp2(ex1,ex2,ex3,ex4) select '{0}','{1}','{2}','{3}'", model.ex1, model.ex2, model.ex3, model.ex4);

                    //sqlStr = string.Format("insert into w_temp2(f1,f2,f3,f4,f5) select '{0}','{1}','{2}'", model.f1, model.f2, model.f3);
                    importCount += conn.Execute(sqlStr);

                }
            }
            return importCount;
        }

    }
}
