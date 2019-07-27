using ExcelDataReader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Data;
using System.IO;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;
using WmsReport.Infrastructure.DbCommon;
using WMSImportData.BLL;
using Microsoft.Extensions.Options;
using Serilog;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using WmsReport.Infrastructure.Config;
using WMSImportData.Config;
using System.Collections.Generic;
using WMSImportData.Model;
using System.Linq;

namespace WMSImportData
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        public static IConfiguration config;
        public static ImportData importData;
        static Program()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "local");

            RegisterServices();
            
            config = new ConfigurationBuilder()
          .AddJsonFile("appsettings.local.json", true, true)
          .Build();

            var service = _serviceProvider.GetService<IWmsDbConnection>();

            importData = new ImportData(service);

            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                if (!string.IsNullOrEmpty(config["ApiConfig:OneColumnFile"]))
                {
                    ReadInData.GetTData(config["ApiConfig:OneColumnFile"], false);
                }
                if (!string.IsNullOrEmpty(config["ApiConfig:FileName"]))
                {
                    ReadInData.GetData(config["ApiConfig:FileName"], true);
                }
                Console.WriteLine("完成导入!");

            }
            catch (Exception ex)
            {
                if (!(ex is FileNotFoundException || ex is ArgumentException ))
                    throw;
                Console.WriteLine(ex.Message);
            }
            
         

        }
        public static void RegisterConfigurationInjectionsAutoFac(IServiceProvider serviceProvider, ContainerBuilder builder)
        {
            builder.Register(context => serviceProvider.GetService<IOptions<ApiConfig>>());
            builder.Register(context => serviceProvider.GetService<IOptions<DbConnections>>());

        }
        static void Main(string[] args)
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);

        }
        private static void RegisterServices()
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (string.IsNullOrWhiteSpace(env))
            {
                // TODO this could fall back to an environment, rather than exceptioning?
                throw new Exception("ASPNETCORE_ENVIRONMENT env variable not set.");
            }
            Console.WriteLine($"Bootstrapping application using environment {env}");

            var configurationBuilder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: false)
             .AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: false)
             .AddEnvironmentVariables();

            var configuration = configurationBuilder.Build();
            var collection = new ServiceCollection();
            collection.AddOptions();
            collection.Configure<ApiConfig>(configuration.GetSection(nameof(ApiConfig)));
            collection.Configure<DbConnections>(configuration.GetSection(nameof(DbConnections)));
            _serviceProvider = collection.BuildServiceProvider();

            ContainerBuilder containerBuilder = new ContainerBuilder();
            RegisterConfigurationInjectionsAutoFac(_serviceProvider, containerBuilder);

            containerBuilder.RegisterType<WmsDbConnection>().As<IWmsDbConnection>();

            //
            // Add other services ...
            //
            containerBuilder.Populate(collection);

            var appContainer = containerBuilder.Build();

            //apiConfig = appContainer.Resolve<IOptions<ApiConfig>>();

            _serviceProvider = new AutofacServiceProvider(appContainer);
        }
         
        public static class ReadInData
        {
            public static void GetData(string path,  bool isFirstRowAsColumnNames = true)
            {
                int importCount = 0;
                DataTableCollection dataTableCollection = ExcelHelper.GetData(path, isFirstRowAsColumnNames);
                if (dataTableCollection.Contains("库存导入"))
                {
                    Console.WriteLine("开始导入库存...");

                    IEnumerable<DataRow> storageinforows = from DataRow row in dataTableCollection["库存导入"].Rows select row;
                    List<StorageinfoTemp> storageinfos = new List<StorageinfoTemp>();
                    foreach (var dt in storageinforows)
                    {
                        StorageinfoTemp storageinfoTemp = new StorageinfoTemp();

                        if (dt.Table.Columns.Contains("单据类型"))
                            storageinfoTemp.OrderType = dt["单据类型"].ToString();
                        if (dt.Table.Columns.Contains("物资编号"))
                            storageinfoTemp.ProductCode = dt["物资编号"].ToString();
                        if (dt.Table.Columns.Contains("采购单号"))
                            storageinfoTemp.CGDID = dt["采购单号"].ToString();
                        if (dt.Table.Columns.Contains("来源单号"))
                            storageinfoTemp.DocEntry = dt["来源单号"].ToString();
                        if (dt.Table.Columns.Contains("权限部门"))
                            storageinfoTemp.DeptCode = dt["权限部门"].ToString();
                        if (dt.Table.Columns.Contains("储位"))
                            storageinfoTemp.Location = dt["储位"].ToString();
                        if (dt.Table.Columns.Contains("项目编码"))
                            storageinfoTemp.ProjectCode = dt["项目编码"].ToString();
                        if (dt.Table.Columns.Contains("供应商编号"))
                            storageinfoTemp.Supplier = dt["供应商编号"].ToString();
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
                        storageinfos.Add(storageinfoTemp);
                    }
                    importCount = importData.ImportStoTb(config["ApiConfig:dept"], storageinfos);
                    Console.WriteLine("库存成功导入" + importCount.ToString() + "条记录!");

                }
                if (dataTableCollection.Contains("物资基础资料"))
                {
                    Console.WriteLine("开始导入物资基础资料...");

                    IEnumerable<DataRow> materialRows = from DataRow row in dataTableCollection["物资基础资料"].Rows select row;
                    List<w_temp_Material> materials = new List<w_temp_Material>();
                    foreach (var dt in materialRows)
                    {
                        w_temp_Material storageinfoTemp = new w_temp_Material();

                        if (dt.Table.Columns.Contains("物资名称"))
                            storageinfoTemp.MaterialName = dt["物资名称"].ToString();
                        if (dt.Table.Columns.Contains("物资编码"))
                            storageinfoTemp.DocEntry = dt["物资编码"].ToString();
                        if (dt.Table.Columns.Contains("单位"))
                            storageinfoTemp.SKU = dt["单位"].ToString();
                        if (dt.Table.Columns.Contains("小类描述"))
                            storageinfoTemp.TypeSmall = dt["小类描述"].ToString();
                        if (dt.Table.Columns.Contains("中类描述"))
                            storageinfoTemp.TypeMid = dt["中类描述"].ToString();
                        if (dt.Table.Columns.Contains("物资大类"))
                            storageinfoTemp.TypeBig = dt["物资大类"].ToString();
                        if (dt.Table.Columns.Contains("供应商编码"))
                            storageinfoTemp.Supplier = dt["供应商编码"].ToString();
                        if (dt.Table.Columns.Contains("供应商名称"))
                            storageinfoTemp.SupName = dt["供应商名称"].ToString();
                        
                        materials.Add(storageinfoTemp);
                    }
                    importCount = importData.ImportMatTb(config["ApiConfig:dept"], materials);
                    Console.WriteLine ("物资基础资料成功导入" + importCount.ToString() + "条记录!");

                }
                if (dataTableCollection.Contains("供应商基础资料"))
                {
                    Console.WriteLine("开始导入供应商基础资料...");

                    IEnumerable<DataRow> materialRows = from DataRow row in dataTableCollection["供应商基础资料"].Rows select row;
                    List<w_temp_Supplier> suppliers = new List<w_temp_Supplier>();
                    foreach (var dt in materialRows)
                    {
                        w_temp_Supplier storageinfoTemp = new w_temp_Supplier();

                       
                        if (dt.Table.Columns.Contains("供应商名称"))
                            storageinfoTemp.SupName = dt["供应商名称"].ToString();
                        if (dt.Table.Columns.Contains("供应商编码"))
                            storageinfoTemp.DocEntry = dt["供应商编码"].ToString();
                        if (dt.Table.Columns.Contains("权限部门"))
                            storageinfoTemp.DeptCode = dt["权限部门"].ToString();

                        suppliers.Add(storageinfoTemp);
                    }
                    importCount = importData.ImportSupplierTb(config["ApiConfig:dept"], suppliers);
                    Console.WriteLine("供应商基础资料成功导入" + importCount.ToString() + "条记录!");

                }
                if (dataTableCollection.Contains("储位基础资料"))
                {
                    Console.WriteLine("开始导入储位基础资料...");

                    IEnumerable<DataRow> storageRows = from DataRow row in dataTableCollection["储位基础资料"].Rows select row;
                    List<w_temp_Storage> temp_Storages = new List<w_temp_Storage>();
                    foreach (var dt in storageRows)
                    {
                        w_temp_Storage w_Temp_Storage = new w_temp_Storage();

                        //int totalColums = dt.Table.Columns.Count;

                        //for (int i = 1; i < totalColums; i++)
                        //{
                        //    if (i == 1)
                        //            w_Temp_Storage.DocEntry = dt[i].ToString();
                        //    if (i == 2)
                        //        w_Temp_Storage.WhCode = dt[i].ToString();

                        //    if (i == 3)
                        //        w_Temp_Storage.DeptCode = dt[i].ToString();

                        //    if (i == 4)
                        //        w_Temp_Storage.SysId = dt[i].ToString();

                        //    w_Temp_Storage.StName = w_Temp_Storage.DocEntry;
                        //}
                        if (dt.Table.Columns.Contains("储位编号"))
                            w_Temp_Storage.DocEntry = dt["储位编号"].ToString();
                        if (dt.Table.Columns.Contains("储位名称"))
                            w_Temp_Storage.StName = dt["储位名称"].ToString();
                        if (dt.Table.Columns.Contains("所属仓库"))
                            w_Temp_Storage.WhCode = dt["所属仓库"].ToString();
                        if (dt.Table.Columns.Contains("权限部门"))
                            w_Temp_Storage.DeptCode = dt["权限部门"].ToString();
                        if (dt.Table.Columns.Contains("sysid"))
                            w_Temp_Storage.SysId = dt["sysid"].ToString();

                        temp_Storages.Add(w_Temp_Storage);
                    }
                    importCount = importData.ImportStorageTb(config["ApiConfig:dept"], temp_Storages);
                    Console.WriteLine("储位基础资料成功导入" + importCount.ToString() + "条记录!");

                }
                if (dataTableCollection.Contains("项目基础资料"))
                {
                    Console.WriteLine("开始导入项目基础资料...");

                    IEnumerable<DataRow> prjRows = from DataRow row in dataTableCollection["项目基础资料"].Rows select row;
                    List<w_temp_Project> projects = new List<w_temp_Project>();
                    foreach (var dt in prjRows)
                    {
                        w_temp_Project w_Temp_Project = new w_temp_Project();


                        if (dt.Table.Columns.Contains("项目编号"))
                            w_Temp_Project.DocEntry = dt["项目编号"].ToString();
                        if (dt.Table.Columns.Contains("项目名称"))
                            w_Temp_Project.PrjName = dt["项目名称"].ToString();
                        if (dt.Table.Columns.Contains("权限部门"))
                            w_Temp_Project.DeptCode = dt["权限部门"].ToString();

                        projects.Add(w_Temp_Project);
                    }
                    importCount = importData.ImportProjectTb(config["ApiConfig:dept"], projects);
                    Console.WriteLine("项目基础资料成功导入" + importCount.ToString() + "条记录!");

                }

            }
            public static void GetTData(string path, bool isFirstRowAsColNames)
            {
                DataTableCollection dataTableCollection = ExcelHelper.GetData(path, isFirstRowAsColNames);
                IEnumerable<DataRow> tempdataRows = null;
                List<w_temp> tempdataList = new List<w_temp>();
                int datarows = 0;
                for (int j = 0; j < dataTableCollection.Count ; j++)
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
                        }
                        tempdataList.Add(w_Temp);
                       
                    }
                    if (j == 0)
                    {
                        datarows = importData.ImportTempSheet(config["ApiConfig:dept"], tempdataList);
                        Console.WriteLine(dataTableCollection[j].TableName+ "成功导入temp" + datarows.ToString() + "条记录!");
                    }
                    if (j == 1)
                    {
                        datarows = importData.ImportTemp2Sheet(config["ApiConfig:dept"], tempdataList);
                        Console.WriteLine(dataTableCollection[j].TableName + "成功导入temp2" + datarows.ToString() + "条记录!");
                    }

                }
            
            }
            
        }


    }

    
}
