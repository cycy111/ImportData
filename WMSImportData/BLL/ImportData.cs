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
using Microsoft.Extensions.Configuration;
using WmsReport.Infrastructure.Config;
using System.Threading.Tasks;
using AccountCenter.Api.Infrastructure.WmsEncryption;
using AccountCenter.Api.Dto.Request;
using Zh.Common.Http;
using Zh.Common.ApiRespose;
using Newtonsoft.Json;

namespace WMSImportData.BLL
{
    public class ImportData: IImportData
    {
        private readonly IWmsDbConnection _wmsDbConnection;

        private readonly IConfiguration _configuration;

        private readonly string constr ;
        public ImportData(IWmsDbConnection wmsDbConnection, IConfiguration configuration)
        {
            _wmsDbConnection = wmsDbConnection;
            _configuration = configuration;
            constr = _wmsDbConnection.GetDbConnStr(_configuration["ApiConfig:dept"].ToString());
        }

        public async Task RunImport(string path,bool isTitle)
        {
            if(isTitle)
                await GetData(path,true);
            else
                await GetTempData(path,false);
        }

        //首行是标题,不导入
        public async Task GetData(string path,bool exitsTitle)
        {
            int importCount = 0;
            DataTableCollection dataTableCollection = ExcelHelper.GetData(path, exitsTitle);
            if ((dataTableCollection.Contains("库存") | dataTableCollection.Contains("库存导入")))
            {
                IEnumerable<DataRow> storageinforows = null;
                Console.WriteLine("开始导入库存...");
                if (dataTableCollection.Contains("库存"))
                    storageinforows = from DataRow row in dataTableCollection["库存"].Rows select row;

                if (dataTableCollection.Contains("库存导入"))
                    storageinforows = from DataRow row in dataTableCollection["库存导入"].Rows select row;

                if (storageinforows.Count() > 0)
                {
                    List<StorageinfoTemp> storageinfos = new List<StorageinfoTemp>();
                    int i = 0;
                    foreach (var dt in storageinforows)
                    {
                        //Console.WriteLine(i++);
                        StorageinfoTemp storageinfoTemp = new StorageinfoTemp();
                        //if (i == 823)
                        //{

                        //}
                        if (dt.Table.Columns.Contains("单据类型"))
                            storageinfoTemp.OrderType = dt["单据类型"].ToString();
                        if (dt.Table.Columns.Contains("物资编号"))
                            storageinfoTemp.ProductCode = dt["物资编号"].ToString();
                        if (dt.Table.Columns.Contains("物资编码"))
                            storageinfoTemp.ProductCode = dt["物资编码"].ToString();
                        if (dt.Table.Columns.Contains("物资名称"))
                            storageinfoTemp.ProductName = dt["物资名称"].ToString();
                        if (dt.Table.Columns.Contains("采购单号"))
                            storageinfoTemp.CGDID = dt["采购单号"].ToString();
                        if (dt.Table.Columns.Contains("采购单行描述"))
                            storageinfoTemp.CGDHMS = dt["采购单行描述"].ToString();
                        if (dt.Table.Columns.Contains("采购单描述"))
                            storageinfoTemp.CGDHMS = dt["采购单描述"].ToString();
                        if (dt.Table.Columns.Contains("来源单号"))
                            storageinfoTemp.DocEntry = dt["来源单号"].ToString();
                        if (dt.Table.Columns.Contains("权限部门"))
                            storageinfoTemp.DeptCode = dt["权限部门"].ToString();
                        if (dt.Table.Columns.Contains("储位"))
                            storageinfoTemp.Location = dt["储位"].ToString();
                        if (dt.Table.Columns.Contains("仓库"))
                            storageinfoTemp.WhCode = dt["仓库"].ToString();
                        if (dt.Table.Columns.Contains("所属仓库"))
                            storageinfoTemp.WhCode = dt["所属仓库"].ToString();
                        if (dt.Table.Columns.Contains("项目编码"))
                            storageinfoTemp.ProjectCode = dt["项目编码"].ToString();
                        if (dt.Table.Columns.Contains("项目编号"))
                            storageinfoTemp.ProjectCode = dt["项目编号"].ToString();
                        if (dt.Table.Columns.Contains("项目名称"))
                            storageinfoTemp.ProjectName = dt["项目名称"].ToString();
                        if (dt.Table.Columns.Contains("供应商编号"))
                            storageinfoTemp.Supplier = dt["供应商编号"].ToString();
                        if (dt.Table.Columns.Contains("供应商编码"))
                            storageinfoTemp.Supplier = dt["供应商编码"].ToString();
                        if (dt.Table.Columns.Contains("供应商名称"))
                            storageinfoTemp.SupName = dt["供应商名称"].ToString();
                        if (dt.Table.Columns.Contains("入库组织"))
                            storageinfoTemp.InOrganize = dt["入库组织"].ToString();
                        if (dt.Table.Columns.Contains("单位"))
                            storageinfoTemp.ProductUnit = dt["单位"].ToString();
                        if (dt.Table.Columns.Contains("物资类别"))
                            storageinfoTemp.ProductType = dt["物资类别"].ToString();
                        if (dt.Table.Columns.Contains("单价"))
                            storageinfoTemp.Price = dt["单价"].ToString();
                        if (dt.Table.Columns.Contains("物资属性"))
                            storageinfoTemp.ProProperty = dt["物资属性"].ToString();
                        if (dt.Table.Columns.Contains("箱号"))
                            storageinfoTemp.PackCode = dt["箱号"].ToString();
                        if (dt.Table.Columns.Contains("装箱单号"))
                            storageinfoTemp.PackingCode = dt["装箱单号"].ToString();
                        if (dt.Table.Columns.Contains("追溯时间"))
                            storageinfoTemp.ZSDate = dt["追溯时间"].ToString();
                        if (dt.Table.Columns.Contains("收货时间"))
                            storageinfoTemp.InSDate = dt["收货时间"].ToString();
                        if (dt.Table.Columns.Contains("项目负责人"))
                            storageinfoTemp.ProjectUser = dt["项目负责人"].ToString();
                        if (dt.Table.Columns.Contains("ERP批次"))
                            storageinfoTemp.ERPNo = dt["ERP批次"].ToString();
                        if (dt.Table.Columns.Contains("物资批次"))
                            storageinfoTemp.ProSeqno = dt["物资批次"].ToString();
                        if (dt.Table.Columns.Contains("关联单号"))
                            storageinfoTemp.PurchaseCode = dt["关联单号"].ToString();
                        if (dt.Table.Columns.Contains("规格"))
                            storageinfoTemp.SpcModel = dt["规格"].ToString();
                        if (dt.Table.Columns.Contains("标准体积"))
                            storageinfoTemp.StandV = dt["标准体积"].ToString();
                        if (dt.Table.Columns.Contains("标准重量"))
                            storageinfoTemp.StandW = dt["标准重量"].ToString();
                        if (dt.Table.Columns.Contains("对应台账"))
                            storageinfoTemp.Taizhang = dt["对应台账"].ToString();
                        if (dt.Table.Columns.Contains("所属部门"))
                            storageinfoTemp.DmadDept = dt["所属部门"].ToString();
                        if (dt.Table.Columns.Contains("MIS单号"))
                            storageinfoTemp.MISOrderID = dt["MIS单号"].ToString();
                        if (dt.Table.Columns.Contains("备注"))
                            storageinfoTemp.Remark1 = dt["备注"].ToString();
                        if (dt.Table.Columns.Contains("可用库存"))
                            storageinfoTemp.Qty = dt["可用库存"].ToString();
                        if (dt.Table.Columns.Contains("厂家合同号"))
                            storageinfoTemp.FacPactCode = dt["厂家合同号"].ToString();
                        if (dt.Table.Columns.Contains("订单合同号"))
                            storageinfoTemp.OrderPactCode = dt["订单合同号"].ToString();
                        if (dt.Table.Columns.Contains("合同号"))
                            storageinfoTemp.PactCode = dt["合同号"].ToString();
                        if (dt.Table.Columns.Contains("需求人"))
                            storageinfoTemp.DmadUser = dt["需求人"].ToString();
                        if (dt.Table.Columns.Contains("地市"))
                            storageinfoTemp.City = dt["地市"].ToString();
                        if (dt.Table.Columns.Contains("套料编号"))
                            storageinfoTemp.ComboNo = dt["套料编号"].ToString();
                        if (dt.Table.Columns.Contains("套料编码"))
                            storageinfoTemp.ComboNo = dt["套料编码"].ToString();
                        if (dt.Table.Columns.Contains("套料名称"))
                            storageinfoTemp.ComboName = dt["套料名称"].ToString();
                        if (dt.Table.Columns.Contains("总价"))
                            storageinfoTemp.Acount = dt["总价"].ToString();
                        if (dt.Table.Columns.Contains("提货单位 "))
                            storageinfoTemp.EnComName = dt["提货单位 "].ToString();
                        if (dt.Table.Columns.Contains("单据日期"))
                            storageinfoTemp.CreateDate = dt["单据日期"].ToString();
                        if (dt.Table.Columns.Contains("长"))
                            storageinfoTemp.xlenght = Convert.ToDecimal((dt["长"]?.ToString()==""?"0": dt["长"]?.ToString()));
                        if (dt.Table.Columns.Contains("宽"))
                            storageinfoTemp.xwidth = Convert.ToDecimal(dt["宽"]?.ToString() == "" ? "0" : dt["宽"]?.ToString());
                        if (dt.Table.Columns.Contains("高"))
                            storageinfoTemp.xheight = Convert.ToDecimal(dt["高"]?.ToString() == "" ? "0" : dt["高"]?.ToString());
                        if (dt.Table.Columns.Contains("实物管理员"))
                            storageinfoTemp.Manager = dt["实物管理员"].ToString();
                        if (dt.Table.Columns.Contains("配比数"))
                            storageinfoTemp.PeiBiQty = dt["配比数"].ToString();
                        if (dt.Table.Columns.Contains("所属账务员"))
                            storageinfoTemp.Accountant = dt["所属账务员"].ToString();
                        if (dt.Table.Columns.Contains("需求人电话"))
                            storageinfoTemp.DmadPhone = dt["需求人电话"].ToString();
                        storageinfos.Add(storageinfoTemp);
                    }
                    
                    using (var conn = GetOpenConnection(constr, DbProvider.SqlServer))
                    {
                        string sqlStr = @"delete from w_StorageInfo_temp";
                        conn.Execute(sqlStr);
                        while (storageinfos.Count > 0)
                        {
                            List<StorageinfoTemp> tempstorageinfos = new List<StorageinfoTemp>();
                            List<DynamicParameters> dynamicParameters = new List<DynamicParameters>();
                            if (storageinfos.Count > 200)
                            {
                                tempstorageinfos = storageinfos.Take(200).ToList();
                                
                                storageinfos.RemoveRange(0, 200);
                            }
                            else
                            {
                                tempstorageinfos = storageinfos.Take(storageinfos.Count).ToList();
                                storageinfos.RemoveRange(0, storageinfos.Count);
                            }
                            try
                            {
                                foreach (var model in tempstorageinfos)
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
                                    dp.Add("@CreateDate", model.CreateDate);
                                    dp.Add("@ProductCategory", model.ProductCategory);
                                    dp.Add("@FirstCate", model.FirstCate);
                                    dp.Add("@SecCate", model.SecCate);
                                    dp.Add("@Location", model.Location);
                                    dp.Add("@PackQty", model.PackQty);
                                    dp.Add("@Qty", model.Qty);
                                    dp.Add("@WhCode", model.WhCode);
                                    dp.Add("@OrderPactCode", model.OrderPactCode);
                                    dp.Add("@FacPactCode", model.FacPactCode);
                                    dp.Add("@DmadUser", model.DmadUser);
                                    dp.Add("@City", model.City);
                                    dp.Add("@xlenght", model.xlenght);
                                    dp.Add("@xwidth", model.xwidth);
                                    dp.Add("@xheight", model.xheight);
                                    dp.Add("@ComboNo", model.ComboNo);
                                    dp.Add("@ComboName", model.ComboName);

                                    dynamicParameters.Add(dp);
                                }
                                 
                                importCount += await conn.ExecuteAsync("w_StorageInfo_temp_ADD", dynamicParameters, commandType: CommandType.StoredProcedure);

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("导入出错!"+ex.Message);
                            }
                        }

                    }
                    Console.WriteLine("库存成功导入" + importCount.ToString() + "条记录!");
                    importCount = 0;
                }


            }
            if (dataTableCollection.Contains("物资基础资料"))
            {
                Console.WriteLine("开始导入物资基础资料...");

                IEnumerable<DataRow> materialRows = from DataRow row in dataTableCollection["物资基础资料"].Rows select row;
                List<w_temp_Material> materials = new List<w_temp_Material>();

                if (materialRows.Count() > 0)
                {
                    foreach (var dt in materialRows)
                    {
                        w_temp_Material storageinfoTemp = new w_temp_Material();

                        if (dt.Table.Columns.Contains("物资名称"))
                            storageinfoTemp.MaterialName = dt["物资名称"].ToString();
                        if (dt.Table.Columns.Contains("物资编号"))
                            storageinfoTemp.DocEntry = dt["物资编号"].ToString();
                        if (dt.Table.Columns.Contains("物资编码"))
                            storageinfoTemp.DocEntry = dt["物资编码"].ToString();
                        if (dt.Table.Columns.Contains("单位"))
                            storageinfoTemp.SKU = dt["单位"].ToString();
                        if (dt.Table.Columns.Contains("小类描述"))
                            storageinfoTemp.TypeSmall = dt["小类描述"].ToString();
                        if (dt.Table.Columns.Contains("中类描述"))
                            storageinfoTemp.TypeMid = dt["中类描述"].ToString();
                        if (dt.Table.Columns.Contains("大类描述"))
                            storageinfoTemp.TypeBig = dt["大类描述"].ToString();
                        if (dt.Table.Columns.Contains("供应商编码"))
                            storageinfoTemp.Supplier = dt["供应商编码"].ToString();
                        if (dt.Table.Columns.Contains("供应商编号"))
                            storageinfoTemp.Supplier = dt["供应商编号"].ToString();
                        if (dt.Table.Columns.Contains("供应商名称"))
                            storageinfoTemp.SupName = dt["供应商名称"].ToString();
                        if (dt.Table.Columns.Contains("套料编码"))
                            storageinfoTemp.ComboNo = dt["套料编码"].ToString();
                        if (dt.Table.Columns.Contains("套料名称"))
                            storageinfoTemp.ComboName = dt["套料名称"].ToString();
                        if (dt.Table.Columns.Contains("映射编码"))
                            storageinfoTemp.MappingCode = dt["映射编码"].ToString();
                        if (dt.Table.Columns.Contains("映射名称"))
                            storageinfoTemp.MappingName = dt["映射名称"].ToString();
                        if (dt.Table.Columns.Contains("权限部门"))
                            storageinfoTemp.DeptCode = dt["权限部门"].ToString();
                        if (dt.Table.Columns.Contains("产品"))
                            storageinfoTemp.MaterialSname = dt["产品"].ToString();
                        if (dt.Table.Columns.Contains("规格"))
                            storageinfoTemp.SpcModel = dt["规格"].ToString();
                        if (dt.Table.Columns.Contains("配比数"))
                            storageinfoTemp.PeiBiQty = dt["配比数"].ToString();
                        materials.Add(storageinfoTemp);
                    }
                    using (var conn = GetOpenConnection(constr, DbProvider.SqlServer))
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
                            dp.Add("@MappingName", model.MappingName);
                            dp.Add("@Price", model.Price);
                            if(!String.IsNullOrEmpty(model.MaterialSname))
                                dp.Add("@MaterialSname", model.MaterialSname);
                            if (!String.IsNullOrEmpty(model.ComboNo))
                                dp.Add("@ComboNo", model.ComboNo);
                            if (!String.IsNullOrEmpty(model.ComboName))
                                dp.Add("@ComboName", model.ComboName);
                            if (!String.IsNullOrEmpty(model.PeiBiQty))
                                dp.Add("@PeiBiQty", model.PeiBiQty);

                            importCount += await conn.ExecuteAsync("w_temp_Material_ADD", dp, commandType: CommandType.StoredProcedure);
                        }

                    }


                    Console.WriteLine("物资基础资料成功导入" + importCount.ToString() + "条记录!");
                    importCount = 0;
                } 
            }
            if (dataTableCollection.Contains("供应商基础资料"))
            {
                Console.WriteLine("开始导入供应商基础资料...");

                IEnumerable<DataRow> supplierRows = from DataRow row in dataTableCollection["供应商基础资料"].Rows select row;
                List<w_temp_Supplier> suppliers = new List<w_temp_Supplier>();
                if (supplierRows.Count() > 0)
                {
                    foreach (var dt in supplierRows)
                    {
                        w_temp_Supplier storageinfoTemp = new w_temp_Supplier();


                        if (dt.Table.Columns.Contains("供应商名称"))
                            storageinfoTemp.SupName = dt["供应商名称"].ToString();
                        if (dt.Table.Columns.Contains("供应商编号"))
                            storageinfoTemp.DocEntry = dt["供应商编号"].ToString();
                        if (dt.Table.Columns.Contains("供应商编码"))
                            storageinfoTemp.DocEntry = dt["供应商编码"].ToString();
                        if (dt.Table.Columns.Contains("权限部门"))
                            storageinfoTemp.DeptCode = dt["权限部门"].ToString();

                        suppliers.Add(storageinfoTemp);
                    }
                    using (var conn = GetOpenConnection(constr, DbProvider.SqlServer))
                    {
                        string sqlStr = @"delete from w_temp_Supplier";
                        conn.Execute(sqlStr);
                        foreach (var model in suppliers)
                        {
                            DynamicParameters dp = new DynamicParameters();

                            dp.Add("@DocEntry", model.DocEntry);
                            dp.Add("@DeptCode", model.DeptCode);
                            dp.Add("@SupName", model.SupName);

                            importCount += await conn.ExecuteAsync("w_temp_Supplier_ADD", dp, commandType: CommandType.StoredProcedure);
                        }

                    }

                }
                
                Console.WriteLine("供应商基础资料成功导入" + importCount.ToString() + "条记录!");

            }
            if (dataTableCollection.Contains("储位基础资料"))
            {
                Console.WriteLine("开始导入储位基础资料...");

                IEnumerable<DataRow> storageRows = from DataRow row in dataTableCollection["储位基础资料"].Rows select row;
                List<w_temp_Storage> temp_Storages = new List<w_temp_Storage>();
                if (storageRows.Count() > 0)
                {
                    foreach (var dt in storageRows)
                    {
                        w_temp_Storage w_Temp_Storage = new w_temp_Storage();

                        if (dt.Table.Columns.Contains("储位编号"))
                            w_Temp_Storage.DocEntry = dt["储位编号"].ToString();
                        if (dt.Table.Columns.Contains("储位编码"))
                            w_Temp_Storage.DocEntry = dt["储位编码"].ToString();
                        if (dt.Table.Columns.Contains("储位名称"))
                            w_Temp_Storage.StName = dt["储位名称"].ToString();
                        if (dt.Table.Columns.Contains("仓库"))
                            w_Temp_Storage.WhCode = dt["仓库"].ToString();
                        if (dt.Table.Columns.Contains("所属仓库"))
                            w_Temp_Storage.WhCode = dt["所属仓库"].ToString();
                        if (dt.Table.Columns.Contains("权限部门"))
                            w_Temp_Storage.DeptCode = dt["权限部门"].ToString();
                        if (dt.Table.Columns.Contains("sysid"))
                            w_Temp_Storage.SysId = dt["sysid"].ToString();
                        if (dt.Table.Columns.Contains("备注"))
                            w_Temp_Storage.Remark = dt["备注"].ToString();
                        temp_Storages.Add(w_Temp_Storage);
                    }
                    using (var conn = GetOpenConnection(constr, DbProvider.SqlServer))
                    {
                        string sqlStr = @"delete from w_temp_Storage";
                        conn.Execute(sqlStr);
                        foreach (var model in temp_Storages)
                        {
                            DynamicParameters dp = new DynamicParameters();

                            dp.Add("@DocEntry", model.DocEntry);
                            dp.Add("@DeptCode", model.DeptCode);
                            dp.Add("@StName", model.StName);
                            dp.Add("@SysId", model.SysId);
                            dp.Add("@WhCode", model.WhCode);
                            dp.Add("@Remark", model.Remark);

                            importCount += await conn.ExecuteAsync("w_temp_Storage_ADD", dp, commandType: CommandType.StoredProcedure);
                        }

                    }

                }
                    
                Console.WriteLine("储位基础资料成功导入" + importCount.ToString() + "条记录!");
                importCount = 0;

            }
            if (dataTableCollection.Contains("项目基础资料"))
            {
                Console.WriteLine("开始导入项目基础资料...");

                IEnumerable<DataRow> prjRows = from DataRow row in dataTableCollection["项目基础资料"].Rows select row;
                List<w_temp_Project> projects = new List<w_temp_Project>();
                if (prjRows.Count() > 0)
                {
                    foreach (var dt in prjRows)
                    {
                        w_temp_Project w_Temp_Project = new w_temp_Project();

                        if (dt.Table.Columns.Contains("项目编码"))
                            w_Temp_Project.DocEntry = dt["项目编码"].ToString();
                        if (dt.Table.Columns.Contains("项目编号"))
                            w_Temp_Project.DocEntry = dt["项目编号"].ToString();
                        if (dt.Table.Columns.Contains("项目名称"))
                            w_Temp_Project.PrjName = dt["项目名称"].ToString();
                        if (dt.Table.Columns.Contains("权限部门"))
                            w_Temp_Project.DeptCode = dt["权限部门"].ToString();
                        if (dt.Table.Columns.Contains("备注"))
                            w_Temp_Project.Remark = dt["备注"].ToString();
                        projects.Add(w_Temp_Project);
                    }
                    using (var conn = GetOpenConnection(constr, DbProvider.SqlServer))
                    {
                        string sqlStr = @"delete from w_temp_Project";
                        conn.Execute(sqlStr);
                        foreach (var model in projects)
                        {
                            DynamicParameters dp = new DynamicParameters();

                            dp.Add("@DocEntry", model.DocEntry);
                            dp.Add("@DeptCode", model.DeptCode);
                            dp.Add("@PrjName", model.PrjName);
                            dp.Add("@Remark", model.Remark);

                            importCount += await conn.ExecuteAsync("w_temp_Project_ADD", dp, commandType: CommandType.StoredProcedure);
                        }

                    }

                    Console.WriteLine("项目基础资料成功导入" + importCount.ToString() + "条记录!");
                    importCount = 0;

                }
            }
            if (dataTableCollection.Contains("仓库基础资料"))
            {
                Console.WriteLine("开始导入仓库基础资料...");

                IEnumerable<DataRow> prjRows = from DataRow row in dataTableCollection["仓库基础资料"].Rows select row;
                if (prjRows.Count() > 0)
                {
                    List<w_temp_Warehouse> warehouses = new List<w_temp_Warehouse>();

                    foreach (var dt in prjRows)
                    {
                        w_temp_Warehouse w_Temp_Warehouse = new w_temp_Warehouse();

                        if (dt.Table.Columns.Contains("仓库编码"))
                            w_Temp_Warehouse.DocEntry = dt["仓库编码"].ToString();
                        if (dt.Table.Columns.Contains("仓库编号"))
                            w_Temp_Warehouse.DocEntry = dt["仓库编号"].ToString();
                        if (dt.Table.Columns.Contains("仓库名称"))
                            w_Temp_Warehouse.WhName = dt["仓库名称"].ToString();
                        if (dt.Table.Columns.Contains("权限部门"))
                            w_Temp_Warehouse.DeptCode = dt["权限部门"].ToString();

                        warehouses.Add(w_Temp_Warehouse);
                    }
                    using (var conn = GetOpenConnection(constr, DbProvider.SqlServer))
                    {
                        string sqlStr = @"delete from w_temp_Warehouse";
                        conn.Execute(sqlStr);
                        foreach (var model in warehouses)
                        {
                            DynamicParameters dp = new DynamicParameters();

                            dp.Add("@DocEntry", model.DocEntry);
                            dp.Add("@DeptCode", model.DeptCode);
                            dp.Add("@WhName", model.WhName);

                            importCount += await conn.ExecuteAsync("w_temp_Warehouse_ADD", dp, commandType: CommandType.StoredProcedure);
                        }

                    }

                }
                Console.WriteLine("仓库基础资料成功导入" + importCount.ToString() + "条记录!");
                importCount = 0;

            }
            if (dataTableCollection.Contains("物资类别基础资料"))
            {
                Console.WriteLine("开始导入物资类别基础资料...");

                IEnumerable<DataRow> prjRows = from DataRow row in dataTableCollection["物资类别基础资料"].Rows select row;
                if (prjRows.Count() > 0)
                {
                    List<w_temp_ProductType> productTypes = new List<w_temp_ProductType>();

                    foreach (var dt in prjRows)
                    {
                        w_temp_ProductType temp_ProductType = new w_temp_ProductType();

                        if (dt.Table.Columns.Contains("编码"))
                            temp_ProductType.AccountID = dt["编码"].ToString();
                        if (dt.Table.Columns.Contains("名称"))
                            temp_ProductType.Name = dt["名称"].ToString();

                        productTypes.Add(temp_ProductType);
                    }
                    using (var conn = GetOpenConnection(constr, DbProvider.SqlServer))
                    {
                        string sqlStr = @"delete from w_temp_ProductType";
                        conn.Execute(sqlStr);
                        foreach (var model in productTypes)
                        {
                            DynamicParameters dp = new DynamicParameters();

                            dp.Add("@AccountID", model.AccountID);
                            dp.Add("@Name", model.Name);

                            importCount += await conn.ExecuteAsync("w_temp_ProductType_ADD", dp, commandType: CommandType.StoredProcedure);
                        }

                    }

                }
                Console.WriteLine("物资类别基础资料成功导入" + importCount.ToString() + "条记录!");
                importCount = 0;

            }
            if (dataTableCollection.Contains("入库组织基础资料"))
            {
                Console.WriteLine("开始导入入库组织基础资料...");

                IEnumerable<DataRow> prjRows = from DataRow row in dataTableCollection["入库组织基础资料"].Rows select row;
                if (prjRows.Count() > 0)
                {
                    List<w_temo_Organize> organizes = new List<w_temo_Organize>();

                    foreach (var dt in prjRows)
                    {
                        w_temo_Organize temo_Organize = new w_temo_Organize();

                        if (dt.Table.Columns.Contains("组织编号"))
                            temo_Organize.KeyCode = dt["组织编号"].ToString();
                        if (dt.Table.Columns.Contains("组织部门名称"))
                            temo_Organize.DptName = dt["组织部门名称"].ToString();
                        if (dt.Table.Columns.Contains("权限部门"))
                            temo_Organize.DeptCode = dt["权限部门"].ToString();
                        organizes.Add(temo_Organize);
                    }
                    using (var conn = GetOpenConnection(constr, DbProvider.SqlServer))
                    {
                        string sqlStr = @"delete from w_temp_Organize";
                        conn.Execute(sqlStr);
                        foreach (var model in organizes)
                        {
                            DynamicParameters dp = new DynamicParameters();

                            dp.Add("@KeyCode", model.KeyCode);
                            dp.Add("@DptName", model.DptName);
                            dp.Add("@DeptCode", model.DeptCode);

                            importCount += await conn.ExecuteAsync("w_temp_Organize_ADD", dp, commandType: CommandType.StoredProcedure);
                        }

                    }

                }
                Console.WriteLine("入库组织基础资料成功导入" + importCount.ToString() + "条记录!");
                importCount = 0;

            }
            if (dataTableCollection.Contains("施工单位基础资料"))
            {
                Console.WriteLine("开始导入施工单位基础资料...");

                IEnumerable<DataRow> engRows = from DataRow row in dataTableCollection["施工单位基础资料"].Rows select row;
                if (engRows.Count() > 0)
                {
                    List<w_temp_EnginCompany> enginComps = new List<w_temp_EnginCompany>();

                    foreach (var dt in engRows)
                    {
                        w_temp_EnginCompany temo_engComp = new w_temp_EnginCompany();

                        if (dt.Table.Columns.Contains("施工单位编号"))
                            temo_engComp.DocEntry = dt["施工单位编号"].ToString();
                        if (dt.Table.Columns.Contains("施工单位名称"))
                            temo_engComp.EngName = dt["施工单位名称"].ToString();
                        if (dt.Table.Columns.Contains("权限部门"))
                            temo_engComp.DeptCode = dt["权限部门"].ToString();
                        if (dt.Table.Columns.Contains("联系人"))
                            temo_engComp.Connector = dt["联系人"].ToString();
                        if (dt.Table.Columns.Contains("固定电话"))
                            temo_engComp.Telephone = dt["固定电话"].ToString();
                        if (dt.Table.Columns.Contains("备注"))
                            temo_engComp.Remark = dt["备注"].ToString();
                        if (dt.Table.Columns.Contains("移动电话"))
                            temo_engComp.Mobile = dt["移动电话"].ToString();
                        if (dt.Table.Columns.Contains("职位"))
                            temo_engComp.Position = dt["职位"].ToString();
                        if (dt.Table.Columns.Contains("部门"))
                            temo_engComp.Department = dt["部门"].ToString();
                        if (dt.Table.Columns.Contains("授权起始日期"))
                            temo_engComp.StartTm = dt["授权起始日期"].ToString();
                        if (dt.Table.Columns.Contains("授权结束日期"))
                            temo_engComp.EndTm = dt["授权结束日期"].ToString();
                        if (dt.Table.Columns.Contains("提货人身份证号"))
                            temo_engComp.ConnectorID = dt["提货人身份证号"].ToString();
                        enginComps.Add(temo_engComp);
                    }
                    using (var conn = GetOpenConnection(constr, DbProvider.SqlServer))
                    {
                        string sqlStr = @"delete from w_temp_EnginCompany";
                        conn.Execute(sqlStr);
                        foreach (var model in enginComps)
                        {
                            DynamicParameters dp = new DynamicParameters();

                            dp.Add("@DocEntry", model.DocEntry);
                            dp.Add("@Remark", model.Remark);
                            dp.Add("@DeptCode", model.DeptCode);
                            dp.Add("@EngName", model.EngName);
                            dp.Add("@ProvinceCode", model.ProvinceCode);
                            dp.Add("@Mobile", model.Mobile);
                            dp.Add("@Department", model.Department);
                            dp.Add("@Position", model.Position);
                            dp.Add("@Connector", model.Connector);
                            dp.Add("@StartTm", model.StartTm);
                            dp.Add("@EndTm", model.EndTm);
                            dp.Add("@ConnectorID", model.ConnectorID);

                            importCount += conn.Execute("w_temp_EnginCompany_ADD", dp, commandType: CommandType.StoredProcedure);
                        }

                    }
                }
                Console.WriteLine("施工单位基础资料成功导入" + importCount.ToString() + "条记录!");
                importCount = 0;

            }
        }

        public async Task GetTempData(string path, bool exitsTitle)
        {
            DataTableCollection dataTableCollection = ExcelHelper.GetData(path, exitsTitle);
            IEnumerable<DataRow> tempdataRows = null;
            List<w_temp> tempdataList = new List<w_temp>();
            int importCount = 0;
            for (int j = 0; j < dataTableCollection.Count; j++)
            {
                tempdataRows = from DataRow row in dataTableCollection[j].Rows select row;
                tempdataList = new List<w_temp>();

                foreach (var dr in tempdataRows)
                {
                    int totalColums = dr.Table.Columns.Count;
                    w_temp w_Temp = new w_temp();

                    for (int i = 0; i < totalColums; i++)
                    {
                        if (i == 0)
                            w_Temp.ex1 = dr[i].ToString();
                        if (i == 1)
                            w_Temp.ex2 = dr[i].ToString();
                        if (i == 2)
                            w_Temp.ex3 = dr[i].ToString();
                        if (i == 3)
                            w_Temp.ex4 = dr[i].ToString();
                        if (i == 4)
                            w_Temp.ex5 = dr[i].ToString();
                        if (i == 5)
                            w_Temp.ex6 = dr[i].ToString();
                        if (i == 6)
                        {
                            
                            string password = WmsEncryptionHelper.Encrypt(dr[0].ToString(), Zh.Common.Cryptography.Md5Helper.Get32MD5One(dr[i].ToString()).ToLower());
                            w_Temp.ex7 = password;

                        }
                        if (i == 7)
                            w_Temp.ex8 = dr[i].ToString();
                        if (i == 8)
                            w_Temp.ex9 = dr[i].ToString();
                    }
                    tempdataList.Add(w_Temp);

                }
               
                if (j == 0)
                {
                    using (var conn = GetOpenConnection(constr, DbProvider.SqlServer))
                    {
                        string sqlStr = @"delete from w_temp";
                        conn.Execute(sqlStr);
                        
                        while (tempdataList.Count > 0)
                        {
                            Array aryPara;
                            if (tempdataList.Count > 200)
                            {
                                aryPara = tempdataList.Take(200).ToArray();
                                tempdataList.RemoveRange(0, 200);
                            }
                            else
                            {
                                aryPara = tempdataList.Take(tempdataList.Count).ToArray();
                                tempdataList.RemoveRange(0, tempdataList.Count);
                            }
                            try
                            {
                                sqlStr = string.Format(@"insert into w_temp(ex1,ex2,ex3,ex4,ex5,ex6,ex7,ex8,ex9)
                                    values(@ex1,@ex2,@ex3,@ex4,@ex5,@ex6,@ex7,@ex8,@ex9)
                                   ");
                                importCount += await conn.ExecuteAsync(sqlStr, aryPara);
                            }
                            catch (Exception ex)
                             {
                                Console.WriteLine("导入出错!"+ex.Message);
                            }
                        }

                    }
                   
                    Console.WriteLine(dataTableCollection[j].TableName + "成功导入temp " + importCount.ToString() + "条记录!");
                    importCount = 0;
                }
                if (j == 1)
                {
                    using (var conn = GetOpenConnection(constr, DbProvider.SqlServer))
                    {
                        string sqlStr = @"delete from w_temp2";
                        conn.Execute(sqlStr);
                        foreach (var model in tempdataList)
                        {
                            //获取表的ID
                            string url = _configuration["Api:GetIdSequence"].ToString() + "tms_user_system/users";
                            string result2 = HttpHelper.HttpGet(url, timeout: 15);
                            ResponseModel<string> res2 = JsonConvert.DeserializeObject<ResponseModel<string>>(result2);
                            string userId = res2.data;
                            DynamicParameters dp = new DynamicParameters();
                            sqlStr = string.Format("insert into w_temp2(ex1,ex2,ex3,ex4,ex5,ex6,ex7,ex8,ex9) select '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}'", model.ex1, model.ex2.Replace("'", "''"), model.ex3,
                                model.ex4, model.ex5, model.ex6, userId, model.ex8, model.ex9);

                            //sqlStr = string.Format("insert into w_temp2(f1,f2,f3,f4,f5) select '{0}','{1}','{2}'", model.f1, model.f2, model.f3);
                            importCount += conn.Execute(sqlStr);
                           
                            //CreateUserByLoginNameDto createUserByLoginName = new CreateUserByLoginNameDto();
                            //createUserByLoginName.loginName = model.ex1;
                            //createUserByLoginName.realName = model.ex2;
                            //createUserByLoginName.mobile = model.ex3;
                            //createUserByLoginName.pwd = model.ex7;


                        }
                    }
                    
                    Console.WriteLine(dataTableCollection[j].TableName + "成功导入temp2 " + importCount.ToString() + "条记录!");
                }

            }

        }


        public int SetRole()
        {
            int importCount = 0;
            string connStr = _configuration["DbConnections:Tus"]?.ToString();
            using (var conn = GetOpenConnection(connStr, DbProvider.Oracle))
            {
                string sql = "select top 1 idx from users where login_name = :loginName";
                var userId = conn.ExecuteScalar<int>(sql, new
                {
                    loginName = "zhangsna"
                });
                sql = "select top 1 idx from role where name = :roleName";
                var roleId = conn.ExecuteScalar<int>(sql, new
                {
                    roleName = "审批人"
                });

                sql = "inset into user_role(role_Id,user_Id) values(:roleId,:userId)";
                conn.Execute(sql, new[]
                {
                    new { roleId=3,userId=5}
                });
                
            }
            return importCount;
        }

    }
}
