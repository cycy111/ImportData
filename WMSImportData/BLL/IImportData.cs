using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WMSImportData.Model;

namespace WMSImportData.BLL
{
    public interface IImportData
    {
        //int ImportStoTb(List<StorageinfoTemp> storageinfos);
        //int ImportMatTb(List<w_temp_Material> materials);
        //int ImportSupplierTb(List<w_temp_Supplier> materials);
        //int ImportStorageTb(List<w_temp_Storage> _Storages);
        //int ImportProjectTb(List<w_temp_Project> projects);
        //int ImportWarehouseTb(List<w_temp_Warehouse> warehouses);
        //int ImportProductType(List<w_temp_ProductType> productTypes);
        //int ImportOrganize(List<w_temo_Organize> organizes);
        //int ImportEnginComp(List<w_temp_EnginCompany> w_Temp_Engins);

        //int ImportTempSheet(List<w_temp> w_Temps);
        //int ImportTemp2Sheet(List<w_temp> w_Temps);
        //int setRole(List<w_temp> w_Temps);
        Task RunImport(string path, bool istitle);
    }
}
